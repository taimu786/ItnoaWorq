import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Login } from './login/login';
import { Register } from './register/register';
import { AuthRoutingModule } from './auth-routing-module';

@NgModule({
  declarations: [Login, Register],
  imports: [
    CommonModule,
    FormsModule,
    AuthRoutingModule
  ],
})
export class AuthModule {}