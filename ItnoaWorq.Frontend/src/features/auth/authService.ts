import api from "../../lib/axios";
import type {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
  RegisterResponse,
} from "../../types/auth";

export const loginApi = async (
  data: LoginRequest
): Promise<LoginResponse> => {
  const res = await api.post<LoginResponse>("/Auth/login", data);
  return res.data;
};

export const registerApi = async (
  data: RegisterRequest
): Promise<RegisterResponse> => {
  const res = await api.post<RegisterResponse>("/Auth/register", data);
  return res.data;
};