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

export type RegisterRequest = {
  email: string;
  password: string;
  fullName?: string;
};

export type RegisterResponse = unknown;

export const AuthService = {
  login: async (data: LoginRequest): Promise<LoginResponse> => {
    // Server must set HttpOnly refresh cookie; response returns access token + user
    const res = await api.post<LoginResponse>("/Auth/login", data);
    return res.data;
  },

  register: async (payload: RegisterRequest): Promise<RegisterResponse> => {
    // backend should return success; if it returns token+user you can adapt
    const res = await api.post<RegisterResponse>("/Auth/register", payload);
    return res.data;
  },

  refresh: async (): Promise<{ token: string }> => {
    // Server reads refresh cookie and returns new access token
    const res = await api.post<{ token: string }>("/Auth/refresh");
    return res.data;
  },

  me: async (): Promise<LoginResponse["user"]> => {
    const res = await api.get<LoginResponse["user"]>("/Auth/me");
    return res.data;
  },

  logout: async (): Promise<void> => {
    // server should clear refresh cookie
    await api.post("/Auth/logout");
  },
};
