import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Profile } from './profile/profile';
import { ProfileRoutingModule } from './profile-routing-module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [Profile],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    FormsModule
  ],
})
export class ProfileModule {}