import "./globals.css";
import AuthBootstrap from "@/components/AuthBootstrap";
import { Toaster } from "@/components/ui/sonner";

export const metadata = { title: "ItnoaWorq" };

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <body>
        <AuthBootstrap>{children}</AuthBootstrap>
        <Toaster />
      </body>
    </html>
  );
}
