import { Component, OnInit } from '@angular/core';
import { AuthService } from 'core/services/auth/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  async ngOnInit() {
    if (!this.authService.isAuthenticated()) {
      console.log('not authenticated at Financial System  login');
      await this.authService.login();
    } else {
      console.log('not authenticated at Financial System  login');
      this.router.navigate(['/dashboard']);
    }
  }
}
