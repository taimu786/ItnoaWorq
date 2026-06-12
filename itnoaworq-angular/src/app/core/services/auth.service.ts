import { Injectable } from '@angular/core';
import { api } from './api.service';
import { LoginResponse, RegisterResponse } from '../../models/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthService {

  async login(email: string, password: string): Promise<LoginResponse> {
    const res = await api.post<LoginResponse>('/Auth/login', {
      email,
      password,
    });

    return res.data;
  }

  async register(
    email: string,
    password: string,
    fullName: string
  ): Promise<RegisterResponse> {
    const res = await api.post<RegisterResponse>('/Auth/register', {
      email,
      password,
      fullName,
    });

    return res.data;
  }
}