import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
})
export class Login {
  email = '';
  password = '';
  loading = false;

  constructor(private auth: AuthService, private router: Router) {}

  async login() {
    try {
      this.loading = true;

      const res = await this.auth.login(this.email, this.password);

      localStorage.setItem('token', res.token);
      localStorage.setItem('user', JSON.stringify(res.user));

      this.router.navigate(['/feed']);
    } catch (err: any) {
      alert(err?.message || 'Login failed');
    } finally {
      this.loading = false;
    }
  }
}