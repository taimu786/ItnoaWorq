// "use client";

// import { useRef } from "react";
// import { useAuthStore } from "@/store/auth/auth.store";

// export default function AuthBootstrap({ children }: { children: React.ReactNode }) {
//   const init = useAuthStore((s) => s.init);
//   const hasRun = useRef(false);

//   // ✅ RUN IMMEDIATELY (before render finishes)
//   if (!hasRun.current) {
//     init();
//     hasRun.current = true;
//   }

//   return <>{children}</>;
// }

"use client";

import { useEffect } from "react";
import { useAuthStore } from "@/store/auth/auth.store";

export default function AuthBootstrap({ children }: { children: React.ReactNode }) {
  const init = useAuthStore((s) => s.init);

  useEffect(() => {
    init();
  }, []);

  return <>{children}</>;
}