// src/lib/axios.ts
import axios, { AxiosError, AxiosRequestConfig } from "axios";
import { getAuthStore } from "@/store/auth/auth.store";
import { AuthService } from "@/services/auth/auth.service";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || "https://localhost:7054/api/",
  headers: { "Content-Type": "application/json" },
  withCredentials: true, // IMPORTANT: send httpOnly refresh cookie on requests
});

// Attach access token from in-memory store (Zustand) for every request
api.interceptors.request.use((config: AxiosRequestConfig) => {
  try {
    const auth = getAuthStore();
    const token = auth.getToken?.();
    if (token && config && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    // Optionally include tenant header if available
    const tenantId = typeof window !== "undefined" ? localStorage.getItem("tenantId") : null;
    if (tenantId && config && config.headers) config.headers["X-Tenant"] = tenantId;
  } catch (err) {
    // ignore
  }
  return config;
});

// Response interceptor: on 401 attempt refresh once, then retry original request
let isRefreshing = false;
let failedQueue: {
  resolve: (value?: any) => void;
  reject: (err?: any) => void;
  config: AxiosRequestConfig;
}[] = [];

const processQueue = (error: any, token: string | null = null) => {
  failedQueue.forEach((p) => {
    if (error) p.reject(error);
    else {
      if (token && p.config.headers) p.config.headers.Authorization = `Bearer ${token}`;
      p.resolve(api.request(p.config));
    }
  });
  failedQueue = [];
};

api.interceptors.response.use(
  (res) => res,
  async (error: AxiosError) => {
    const originalRequest = error.config!;
    if (error.response?.status === 401 && !originalRequest.headers!["x-retry"]) {
      if (isRefreshing) {
        // queue the request
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject, config: originalRequest });
        });
      }

      isRefreshing = true;
      try {
        // call refresh endpoint; since refresh token is in httpOnly cookie,
        // this call should include credentials; server returns new access token
        const refreshResponse = await AuthService.refresh();
        const newToken = refreshResponse.token;
        // update in-memory store
        const auth = getAuthStore();
        auth.setToken(newToken);

        // mark as retried so we don't loop
        originalRequest.headers!["x-retry"] = "1";
        if (originalRequest.headers) originalRequest.headers.Authorization = `Bearer ${newToken}`;

        processQueue(null, newToken);
        return api.request(originalRequest);
      } catch (refreshErr) {
        processQueue(refreshErr, null);
        // if refresh failed -> force logout
        const auth = getAuthStore();
        auth.logout();
        return Promise.reject(refreshErr);
      } finally {
        isRefreshing = false;
      }
    }

    return Promise.reject(error);
  }
);

export default api;
