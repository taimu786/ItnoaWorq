import { Injectable } from '@angular/core';
import { api } from './api.service';
import { ProfileDto } from '../../models/profile.model';

@Injectable({ providedIn: 'root' })
export class ProfileService {

  async getProfile(): Promise<ProfileDto> {
    const res = await api.get<ProfileDto>('/Profiles/me');
    return res.data;
  }

  async updateProfile(profile: ProfileDto) {
    await api.put('/Profiles/me', profile);
  }
}