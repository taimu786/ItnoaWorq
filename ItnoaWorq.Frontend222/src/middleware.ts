// src/middleware.ts
import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

export function middleware(req: NextRequest) {
  const url = req.nextUrl.clone();
  const token = req.cookies.get("accessToken")?.value || null;
  const refresh = req.cookies.get("refreshToken")?.value || null;

  // Protect dashboard routes (server-side)
  if (req.nextUrl.pathname.startsWith("/dashboard")) {
    // If we use access token cookie (optional)
    if (!token && !refresh) {
      url.pathname = "/auth/login";
      return NextResponse.redirect(url);
    }
    // otherwise allow (the frontend/axios will attempt refresh if needed)
  }

  return NextResponse.next();
}

export const config = {
  matcher: ["/dashboard/:path*", "/account/:path*"], // pages to protect server-side
};
