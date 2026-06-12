"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuthStore } from "@/store/auth/auth.store";

export default function DashboardLayout({ children }: { children: React.ReactNode }) {
  const router = useRouter();
  const { user, initialized } = useAuthStore();

  console.log("LAYOUT STATE:", { user, initialized });

  useEffect(() => {
    if (!initialized) return; // 🔥 WAIT

    if (!user) {
      console.log("REDIRECTING TO LOGIN");
      router.replace("/auth/login");
    }
  }, [initialized, user]);

  // 🔥 DO NOT RENDER ANYTHING until initialized
  if (!initialized) {
    return <div className="p-6">Initializing...</div>;
  }

  // 🔥 IMPORTANT: only block AFTER initialized
  if (!user) {
    return <div className="p-6">Redirecting...</div>;
  }

  return (
    <div className="flex">
      <aside className="w-64 border-r p-4">Sidebar</aside>
      <main className="flex-1 p-6">{children}</main>
    </div>
  );
}