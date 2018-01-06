import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {Injectable} from '@angular/core';
import {OAuthService} from 'angular-oauth2-oidc';
@Injectable()
export class MainPageGuard implements CanActivate {
  constructor(private oAuthService: OAuthService, private router: Router) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.oAuthService.hasValidAccessToken()) {
      this.router.navigate(['/dashboard']);
      return false;
    } else {
      return true;
    }
  }

}
