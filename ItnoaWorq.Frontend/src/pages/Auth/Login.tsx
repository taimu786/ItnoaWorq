import { useState } from "react";
import { useDispatch } from "react-redux";
import { setAuth } from "../../features/auth/authSlice";
import { loginApi } from "../../features/auth/authService";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";

export default function Login() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const res = await loginApi({ email, password });

      dispatch(setAuth(res));
      toast.success("Welcome back!");

      navigate("/dashboard");
    } catch {
      toast.error("Invalid credentials");
    }
  };

  return (
    <div className="flex min-h-screen">
      
      {/* LEFT SIDE (IMAGE) */}
      <div className="hidden md:block w-1/2">
        <img
          src="\Login BG.jpg" // 👈 put image in public folder
          alt="Login"
          className="h-full w-full object-cover"
        />
      </div>

      {/* RIGHT SIDE (FORM) */}
      <div className="flex w-full md:w-1/2 items-center justify-center bg-white px-8">
        
        <div className="w-full max-w-md">
          
          {/* TOP TEXT */}
          <h1 className="text-3xl font-bold mb-2">
            Welcome to ItnoaWorq 👋
          </h1>

          <p className="text-gray-500 mb-6">
            From Hire to Retire — manage everything in one place.
          </p>

          {/* INPUTS */}
          <input
            type="email"
            placeholder="Email"
            onChange={(e) => setEmail(e.target.value)}
            className="w-full p-3 border rounded-lg mb-4 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />

          <input
            type="password"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
            className="w-full p-3 border rounded-lg mb-4 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />

          {/* BUTTON */}
          <button
            onClick={handleLogin}
            className="w-full bg-indigo-600 text-white p-3 rounded-lg hover:bg-indigo-700 transition"
          >
            Sign In
          </button>

          {/* FOOTER */}
          <p className="text-sm text-center mt-4">
            Don’t have an account?{" "}
            <span
              onClick={() => navigate("/register")}
              className="text-indigo-600 cursor-pointer hover:underline"
            >
              Register
            </span>
          </p>

        </div>
      </div>
    </div>
  );
}