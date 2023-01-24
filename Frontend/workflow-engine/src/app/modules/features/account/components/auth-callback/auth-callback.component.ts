import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { AuthService } from 'core/services/auth/auth.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  async ngOnInit() {
    await this.authService.completeAuthentication();
    this.router.navigateByUrl('/main/home');
  }
}
