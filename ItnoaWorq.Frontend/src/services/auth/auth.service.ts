// src/services/auth/auth.service.ts
import api from "@/lib/axios";

export type LoginRequest = { email: string; password: string; rememberMe?: boolean };
export type LoginResponse = {
  token: string; // short-lived access token (returned by backend)
  user: {
    id: string;
    email: string;
    fullName?: string;
    roles?: string[];
  };
};

export const AuthService = {
  login: async (data: LoginRequest): Promise<LoginResponse> => {
    // Server must set HttpOnly refresh cookie; response returns access token + user
    const res = await api.post<LoginResponse>("/Authentication/login", data);
    return res.data;
  },

  register: async (payload: any): Promise<any> => {
    // backend should return success; if it returns token+user you can adapt
    const res = await api.post("/Authentication/register", payload);
    return res.data;
  },

  refresh: async (): Promise<{ token: string }> => {
    // Server reads refresh cookie and returns new access token
    const res = await api.post<{ token: string }>("/Authentication/refresh");
    return res.data;
  },

  me: async (): Promise<LoginResponse["user"]> => {
    const res = await api.get<LoginResponse["user"]>("/Authentication/me");
    return res.data;
  },

  logout: async (): Promise<void> => {
    // server should clear refresh cookie
    await api.post("/Authentication/logout");
  },
};
