// src/components/AuthBootstrap.tsx
"use client";
import { useEffect } from "react";
// import { usePathname } from "next/navigation";
import { useAuthStore } from "@/store/auth/auth.store";

export default function AuthBootstrap({ children }: { children?: React.ReactNode }) {
  const init = useAuthStore((s) => s.init);
  const loading = useAuthStore((s) => s.loading);
  // const pathname = usePathname();

  useEffect(() => {
    // initialize auth (refresh token -> me) only once on client load
    init().catch(() => {});
  }, [init]);

  // optionally, while init is running, show nothing (or a spinner)
  if (loading) return <div className="min-h-screen flex items-center justify-center">Loading...</div>;

  return <>{children}</>;
}
