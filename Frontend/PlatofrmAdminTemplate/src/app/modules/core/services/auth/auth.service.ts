import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ConfigService } from '../config/config.service';
import { UserManager, User } from 'oidc-client';
import { Router } from '@angular/router';
import { Shell } from 'base/components/shell';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})

/**
 * Auth Services
 * the main service for authentications
 */
export class AuthService {
  // Observable navItem source
  private authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this.authNavStatusSource.asObservable();
  private userPermissions: BehaviorSubject<string[] | any> = new BehaviorSubject(null);

  private manager = new UserManager({
    authority: this.configService.getAppUrl('ID4'),
    client_id: 'FINANCIAL-SYSTEM',
    redirect_uri: this.configService.getAppUrl('REDIRECT-URL'),
    post_logout_redirect_uri: this.configService.getAppUrl('LOGOUT-URL'),
    response_type: 'code',
    scope: 'openid profile email UserManagementApi FinancialApi FileManagerApi',
    filterProtocolClaims: true,
    loadUserInfo: true,
    revokeAccessTokenOnSignout: true
  });

  private user: User | null;
  get HttpService(): HttpClient { return Shell.Injector.get(HttpClient); }
  constructor(
    private configService: ConfigService,
    private router: Router
  ) {
    this.manager.getUser().then(user => {
      this.user = user;
      this.authNavStatusSource.next(this.isAuthenticated());
    });

    this.observerSignOut();
    this.accessTokenExpiring();
    this.accessTokenExpired();
  }

  get userPermissions$(): Observable<string[]> {
    if (this.userPermissions.value) {
      return this.userPermissions.asObservable();
    }
  }


  login(newAccount?: boolean, userName?: string) {

    const extraQueryParams = newAccount && userName ? {
      newAccount,
      userName
    } : {};

    // https://github.com/IdentityModel/oidc-client-js/issues/315
    return this.manager.signinRedirect({
      extraQueryParams
    });
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    Shell.Session.StartSession(this.user);
    this.getUserPermissions(this.user);
    this.authNavStatusSource.next(this.isAuthenticated());
  }

  getUserPermissions(user?: User): void {
    const url = this.configService.getAppUrl('UserManagementApi');
    this.HttpService
      .get(url + 'UserClaims/GetByUserIdAsync/' +
        user.profile.sub +
        '/' +
        user.profile.idp +
        '/' +
        user.profile.appCode
      )
      .subscribe((res: any) => {
        Shell.Session.SetPermissions(res.data);
      });
  }
  isAuthenticated(): boolean {
    if (this.user === undefined) {
      return true;
    } else {
      return this.user !== null && !this.user.expired;
    }
  }

  get User(): User {
    return this.user ? this.user : null;
  }

  get authorizationHeaderValue(): string {
    return this.user ? `${this.user.token_type} ${this.user.access_token}` : null;
  }

  get role(): string {
    return this.user != null ? this.user.profile['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] : '';
  }

  get name(): string {
    return this.user != null ? this.user.profile.name : '';
  }
  get arabicName(): string {
    return this.user != null ? this.user.profile.arabicName : '';
  }
  get givenName(): string {
    return this.user != null ? this.user.profile.given_name : '';
  }

  get accessToken(): string {
    return this.user ? this.user.access_token : null;
  }

  async signOut() {
    Shell.Session.EndSession();
    await this.manager.signoutRedirect();
    sessionStorage.clear();
  }

  observerSignOut() {
    this.manager.events.addUserSignedOut(() => {
      this.user = null;
      Shell.Session.EndSession();
      sessionStorage.clear();
      this.router.navigate(['/login']);
    });
  }

  accessTokenExpiring(): void {
    this.manager.events.addAccessTokenExpiring(() => {
      console.log('access token is about to expire');
    });
  }

  accessTokenExpired(): void {
    this.manager.events.addAccessTokenExpired(() => {
      console.log('access token is  expired');
    });
  }




}
