export interface User {
  id: string;
  email: string;
  fullName?: string | null;
}

export interface LoginResponse {
  token: string;
  user: User;
}

export interface RegisterResponse {
  token: string;
  exp: string;
  userId: string;
}