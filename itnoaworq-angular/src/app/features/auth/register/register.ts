import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
})
export class Register {
  email = '';
  password = '';
  fullName = '';
  loading = false;

  constructor(private auth: AuthService, private router: Router) {}

  async register() {
    try {
      this.loading = true;

      const res = await this.auth.register(
        this.email,
        this.password,
        this.fullName
      );

      localStorage.setItem('token', res.token);

      this.router.navigate(['/feed']);
    } catch (err: any) {
      alert(err?.message || 'Register failed');
    } finally {
      this.loading = false;
    }
  }
}