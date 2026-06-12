import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from "../pages/Auth/Login";
import Dashboard from "../pages/Dashboard";
import { useSelector } from "react-redux";
import Register from "../pages/Auth/Register";
import FeedPage from "../pages/Feed/FeedPage";
import NetworkPage from "../pages/Network/NetworkPage";
import JobsPage from "../pages/Jobs/JobsPage";
import ProfilePage from "../pages/Profile/ProfilePage";

const PrivateRoute = ({ children }: any) => {
  const token = useSelector((state: any) => state.auth.token);

  return token ? children : <Navigate to="/login" />;
};

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/feed" element={<FeedPage />} />
        <Route path="/network" element={<NetworkPage />} />
        <Route path="/jobs" element={<JobsPage />} />
        <Route path="/profile" element={<ProfilePage />} />

        <Route
          path="/dashboard"
          element={
            <PrivateRoute>
              <Dashboard />
            </PrivateRoute>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}