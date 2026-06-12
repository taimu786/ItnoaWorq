import { useNavigate } from "react-router-dom";

export default function Navbar() {
  const navigate = useNavigate();

  return (
    <div className="fixed top-0 left-0 w-full bg-white shadow z-50">
      <div className="max-w-6xl mx-auto flex items-center justify-between p-3">

        {/* LOGO */}
        <h1
          className="text-xl font-bold text-indigo-600 cursor-pointer"
          onClick={() => navigate("/feed")}
        >
          ItnoaWorq
        </h1>

        {/* SEARCH */}
        <input
          placeholder="Search..."
          className="hidden md:block w-1/3 p-2 border rounded-lg"
        />

        {/* NAV ITEMS */}
        <div className="flex gap-6 text-gray-600">
          <span className="cursor-pointer" onClick={() => navigate("/feed")}>Feed</span>
          <span className="cursor-pointer" onClick={() => navigate("/network")}>Network</span>
          <span className="cursor-pointer" onClick={() => navigate("/jobs")}>Jobs</span>
          <span className="cursor-pointer" onClick={() => navigate("/profile")}>Profile</span>
        </div>

      </div>
    </div>
  );
}