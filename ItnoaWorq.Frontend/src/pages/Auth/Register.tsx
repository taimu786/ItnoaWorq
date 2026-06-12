import { useState } from "react";
import { useDispatch } from "react-redux";
import { setAuth } from "../../features/auth/authSlice";
import { registerApi } from "../../features/auth/authService";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";

export default function Register() {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [fullName, setFullName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleRegister = async () => {
        try {
            if (password.length < 6) {
                toast.error("Password too short");
                return;
            }
            const res = await registerApi({ email, password, fullName });

            // 🔥 Map backend response → frontend user
            const user = {
                id: res.userId,
                email,
                fullName,
            };

            dispatch(setAuth({ user, token: res.token }));

            toast.success("Account created successfully!");

            navigate("/dashboard");
        } catch (err: any) {
            toast.error("Registration failed");
        }
    };

    return (
        <div className="flex min-h-screen">

            {/* LEFT SIDE IMAGE */}
            <div className="hidden md:block w-1/2">
                <img
                    src="/Register_banner.png"
                    alt="Register"
                    className="h-full w-full object-cover"
                />
            </div>

            {/* RIGHT SIDE FORM */}
            <div className="flex w-full md:w-1/2 items-center justify-center bg-white px-8">
                <div className="w-full max-w-md">

                    {/* TOP TEXT */}
                    <h1 className="text-3xl font-bold mb-2">
                        Create your account 🚀
                    </h1>

                    <p className="text-gray-500 mb-6">
                        Join ItnoaWorq and start managing everything in one place.
                    </p>

                    {/* FULL NAME */}
                    <input
                        type="text"
                        placeholder="Full Name"
                        onChange={(e) => setFullName(e.target.value)}
                        className="w-full p-3 border rounded-lg mb-4 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                    />

                    {/* EMAIL */}
                    <input
                        type="email"
                        placeholder="Email"
                        onChange={(e) => setEmail(e.target.value)}
                        className="w-full p-3 border rounded-lg mb-4 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                    />

                    {/* PASSWORD */}
                    <input
                        type="password"
                        placeholder="Password"
                        onChange={(e) => setPassword(e.target.value)}
                        className="w-full p-3 border rounded-lg mb-4 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                    />

                    {/* BUTTON */}
                    <button
                        onClick={handleRegister}
                        className="w-full bg-indigo-600 text-white p-3 rounded-lg hover:bg-indigo-700 transition"
                    >
                        Create Account
                    </button>

                    {/* FOOTER */}
                    <p className="text-sm text-center mt-4">
                        Already have an account?{" "}
                        <span
                            onClick={() => navigate("/login")}
                            className="text-indigo-600 cursor-pointer hover:underline"
                        >
                            Sign in
                        </span>
                    </p>

                </div>
            </div>
        </div>
    );
}