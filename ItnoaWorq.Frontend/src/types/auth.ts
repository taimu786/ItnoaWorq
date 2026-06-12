export interface User {
  id: string;
  email: string;
  fullName?: string | null;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  user: User;
}

export interface AuthResponse {
  token: string;
  exp: string;
  userId: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  fullName: string;
}

export interface RegisterResponse {
  token: string;
  exp: string;
  userId: string;
}