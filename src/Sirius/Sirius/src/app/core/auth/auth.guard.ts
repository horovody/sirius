import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {AuthService} from './auth.service';
import {Observable} from 'rxjs/Observable';
import {concatMap, map} from 'rxjs/operators';
/**
 * Decides if a route can be activated.
 */
@Injectable()
export class AuthGuard implements CanActivate {

  private signedIn: boolean;

  constructor(private authService: AuthService, private router: Router) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    return this.authService.isSignedIn().pipe(
      map((signedIn: boolean) => { this.signedIn = signedIn; }),
      concatMap(() => this.authService.userChanged().pipe(
        map(() => {
          const url: string = state.url;
          // TODO need to pass roles from route and check here
          if (this.signedIn) {
            return true;
          }

          // Stores the attempted URL for redirecting.
          this.authService.redirectUrl = url;

          // Not signed in so redirects to signin page.
          this.router.navigate(['/account/signin']);
          return false;
        })
      ))
    );
  }

}
