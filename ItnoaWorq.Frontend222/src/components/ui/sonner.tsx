"use client";

import { Toaster as Sonner } from "sonner";

export function Toaster() {
  return (
    <Sonner
      richColors
      position="top-right"
      toastOptions={{
        duration: 3500,
        className: "text-sm",
      }}
    />
  );
}
