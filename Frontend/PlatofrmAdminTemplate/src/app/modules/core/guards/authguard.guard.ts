import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { RouteData } from './models';
import { SessionManager } from './session-manager';
import { Observable } from 'rxjs';
import { AuthService } from 'core/services/auth/auth.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    /* get current session manager */
    const manager: SessionManager = SessionManager.Current();
    /* if user is not logged in return false and redirect to login */
    if (!this.authService.isAuthenticated() || manager.GetToken() == null) {
      this.router.navigate(['/account/login'], { queryParams: { redirect: state.url }, replaceUrl: true });
    }
    /* if there is no data in route allow the user */
    if (route.data === undefined) {
      return true;
    }
    /* get route data */
    const data: RouteData = Object.assign(new RouteData(), route.data);
    /* if it allow anonymous allow the user */
    if (data.IsAnonymous) {
      return true;
    }
    /* check if user ia authorized */
    const result = manager.IsAuthorized(data);
    if (result) {
      return true;
    } else {
      this.router.navigate(['/main/403']);
      return false;
    }

  }
}
