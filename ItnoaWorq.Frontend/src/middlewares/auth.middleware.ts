// src/middlewares/auth.middleware.ts
import { getAuthStore } from "@/store/auth/auth.store";
// import { useRouter } from "next/navigation";

export const ensureAuthClient = (redirectTo = "/auth/login") => {
  const auth = getAuthStore();
  const token = auth.getToken();
  if (!token && typeof window !== "undefined") {
    // redirect using client-side navigation
    window.location.href = redirectTo;
  }
};
