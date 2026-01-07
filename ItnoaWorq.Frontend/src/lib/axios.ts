// src/lib/axios.ts
import axios, {
  AxiosError,
  InternalAxiosRequestConfig,
} from "axios";

import { getAuthStore } from "@/store/auth/auth.store";
import { AuthService } from "@/services/auth/auth.service";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || "https://localhost:7054/api/",
  headers: { "Content-Type": "application/json" },
  withCredentials: true, // IMPORTANT: send httpOnly refresh cookie on requests
});

// Attach access token from in-memory store (Zustand) for every request
api.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  try {
    const auth = getAuthStore();
    const token = auth.getToken?.();
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    const tenantId =
      typeof window !== "undefined" ? localStorage.getItem("tenantId") : null;
    if (tenantId && config.headers) {
      config.headers["X-Tenant"] = tenantId;
    }
  } catch {
    // ignore
  }
  return config;
});

// Response interceptor: on 401 attempt refresh once, then retry original request
let isRefreshing = false;
let failedQueue: {
  resolve: (value?: unknown) => void;
  reject: (err?: unknown) => void;
  config: InternalAxiosRequestConfig;
}[] = [];

const processQueue = (error: unknown, token: string | null = null) => {
  failedQueue.forEach((p) => {
    if (error) {
      p.reject(error);
    } else {
      if (token && p.config.headers) {
        p.config.headers.Authorization = `Bearer ${token}`;
      }
      p.resolve(api.request(p.config));
    }
  });
  failedQueue = [];
};

api.interceptors.response.use(
  (res) => res,
  async (error: AxiosError) => {
    const originalRequest = error.config as InternalAxiosRequestConfig;

    if (error.response?.status === 401 && !originalRequest.headers?.["x-retry"]) {
      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject, config: originalRequest });
        });
      }

      isRefreshing = true;
      try {
        const refreshResponse = await AuthService.refresh();
        const newToken = refreshResponse.token;

        const auth = getAuthStore();
        auth.setToken(newToken);

        originalRequest.headers = originalRequest.headers || {};
        originalRequest.headers["x-retry"] = "1";
        originalRequest.headers.Authorization = `Bearer ${newToken}`;

        processQueue(null, newToken);
        return api.request(originalRequest);
      } catch (refreshErr: unknown) {
        processQueue(refreshErr, null);
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
