import Navbar from "./Navbar";

export default function MainLayout({ children, layout = "feed" }: any) {
  return (
    <div className="bg-gray-100 min-h-screen">

      <Navbar />

      <div className="max-w-6xl mx-auto flex gap-6 pt-20 px-4">

        {/* FEED LAYOUT */}
        {layout === "feed" && (
          <>
            <div className="hidden lg:block w-1/4" />
            <div className="w-full lg:w-2/4">{children}</div>
            <div className="hidden lg:block w-1/4" />
          </>
        )}

        {/* PROFILE LAYOUT */}
        {layout === "profile" && (
          <>
            <div className="w-full lg:w-4/5">{children}</div>
            <div className="hidden lg:block w-1/5" />
          </>
        )}

      </div>
    </div>
  );
}