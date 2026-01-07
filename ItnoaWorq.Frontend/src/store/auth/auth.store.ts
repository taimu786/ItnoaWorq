// src/store/auth/auth.store.ts
import { create } from "zustand";
import { AuthService, LoginRequest, RegisterRequest } from "@/services/auth/auth.service";

export type User = {
  id: string;
  email: string;
  fullName?: string;
  roles?: string[];
};

type AuthState = {
  user: User | null;
  token: string | null; // kept in memory only
  loading: boolean;
  setToken: (t: string | null) => void;
  login: (payload: LoginRequest) => Promise<void>;
  register: (payload: RegisterRequest) => Promise<void>;
  logout: () => Promise<void>;
  init: () => Promise<void>; // attempt bootstrap (refresh -> me)
  getToken: () => string | null;
};

export const useAuthStore = create<AuthState>((set, get) => ({
  user: null,
  token: null,
  loading: false,

  setToken: (t) => set({ token: t }),

  login: async (payload) => {
    set({ loading: true });
    try {
      const res = await AuthService.login(payload);
      set({ user: res.user, token: res.token });
      // persist user (non-sensitive) so UI can show name after refresh attempts
      if (typeof window !== "undefined") localStorage.setItem("user", JSON.stringify(res.user));
    } finally {
      set({ loading: false });
    }
  },

  register: async (payload: RegisterRequest) => {
    set({ loading: true });
    try {
      await AuthService.register(payload);
      // after register, try to login automatically (backend may also return token)
      await get().login({ email: payload.email, password: payload.password });
    } finally {
      set({ loading: false });
    }
  },

  logout: async () => {
    set({ loading: true });
    try {
      await AuthService.logout();
    } finally {
      set({ user: null, token: null, loading: false });
      if (typeof window !== "undefined") {
        localStorage.removeItem("user");
        localStorage.removeItem("tenantId");
      }
    }
  },

  init: async () => {
    // called on app bootstrap to try refresh if refresh cookie present
    set({ loading: true });
    try {
      // if we already have in-memory token, nothing to do
      if (get().token) return;

      // Try refresh: this will use axios (withCredentials) and server will read refresh cookie
      const refreshResp = await AuthService.refresh();
      if (refreshResp?.token) {
        set({ token: refreshResp.token });
        // get profile
        try {
          const u = await AuthService.me();
          set({ user: u });
          if (typeof window !== "undefined") localStorage.setItem("user", JSON.stringify(u));
        } catch {
          set({ user: null });
        }
      } else {
        // if refresh did not return token, try to restore user only
        const u = typeof window !== "undefined" ? localStorage.getItem("user") : null;
        if (u) set({ user: JSON.parse(u) });
      }
    } catch {
      // ignore - user is not authenticated
      const u = typeof window !== "undefined" ? localStorage.getItem("user") : null;
      if (u) set({ user: JSON.parse(u) });
    } finally {
      set({ loading: false });
    }
  },

  getToken: () => get().token,
}));

// Helper for non-React code (axios interceptors)
export const getAuthStore = () => ({
  getToken: () => {
    try {
      return useAuthStore.getState().token as string | null;
    } catch {
      return null;
    }
  },
  setToken: (t: string | null) => {
    try {
      useAuthStore.setState({ token: t });
    } catch {}
  },
  logout: async () => {
    try {
      await useAuthStore.getState().logout();
    } catch {}
  },
});
