import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../../core/services/profile.service';
import { ProfileDto } from '../../../models/profile.model';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.html',
})
export class Profile implements OnInit {

  profile!: ProfileDto;

  constructor(private profileService: ProfileService) {}

  async ngOnInit() {
    this.profile = await this.profileService.getProfile();
  }
}