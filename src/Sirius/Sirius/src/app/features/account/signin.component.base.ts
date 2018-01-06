import {Router} from '@angular/router';
import {OAuthService} from 'angular-oauth2-oidc';
import {AuthService} from '../../core/auth/auth.service';


export abstract class SigninComponentBase {
  model: any = {};
  errorMessages: any[] = [];

  constructor(
    protected router: Router,
    protected oAuthService: OAuthService,
    protected authService: AuthService) {
  }

  signin(): void {
    this.oAuthService
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(this.model.username, this.model.password)
      .then(() => {
        this.authService.init();

        // Strategy for refresh token through a scheduler.
        this.authService.scheduleRefresh();

        // Gets the redirect URL from authentication service.
        // If no redirect has been set, uses the default.
        const redirect: string = this.authService.redirectUrl
          ? this.authService.redirectUrl
          : '/home';
        // Redirects the user.
        this.router.navigate([redirect]);
      })
      .catch((error: any) => {
        // Checks for error in response (error from the Token endpoint).
        if (error.error !== '') {
          const body: any = error.error;

          switch (body.error) {
            case 'invalid_grant':
              this.errorMessages.push({ description: 'Invalid email or password.' });
              break;
            default:
              this.errorMessages.push({ description: 'Unexpected error. Try again.' });
          }
        } else {
          const errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
          console.log(errMsg);
          this.errorMessages.push({ description: 'Server error. Try later.' });
        }
      });
  }

  clearMessages(): void {
    this.errorMessages = [];
  }
}
