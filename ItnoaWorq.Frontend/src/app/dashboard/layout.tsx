// src/app/dashboard/layout.tsx
"use client";
import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuthStore } from "@/store/auth/auth.store";

export default function DashboardLayout({ children }: { children: React.ReactNode }) {
  const token = useAuthStore((s) => s.token);
  const loading = useAuthStore((s) => s.loading);
  const user = useAuthStore((s) => s.user);
  const router = useRouter();

  useEffect(() => {
    // while initializing, do nothing
    if (loading) return;
    // if no token and no user, redirect to login (AuthBootstrap might have attempted refresh)
    if (!token && !user) {
      router.push("/auth/login");
    }
  }, [token, user, loading, router]);

  // optionally show a spinner while waiting for init
  if (loading) return <div className="min-h-screen flex items-center justify-center">Loading…</div>;

  // Only render children if user or token
  return token || user ? <>{children}</> : null;
}
