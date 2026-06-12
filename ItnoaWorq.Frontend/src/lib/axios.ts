import axios from "axios";
import { ENV } from "../config/env";

const api = axios.create({
  baseURL: ENV.API_BASE_URL,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export default api;