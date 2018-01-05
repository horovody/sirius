import {OAuthService} from 'angular-oauth2-oidc';
import {Router} from '@angular/router';
import {AuthService} from '../../../core/auth/auth.service';
import {Component} from '@angular/core';
import {AccountService} from '../../../core/auth/account.service';
@Component({
  templateUrl: './signup.component.html'
})
export class SignupComponent {
  model: any = {};
  errorMessages: any[] = [];

  constructor(
    protected router: Router,
    protected oAuthService: OAuthService,
    protected authService: AuthService,
    protected accountService: AccountService) {
  }

  signup(): void {
    this.accountService.register(this.model)
      .subscribe(
        (res: any) => {
          if (res.succeeded) {
            // Signs in the user.
            this.signin();
          } else {
            this.errorMessages = res.errors;
          }
        },
        (error: any) => {
          const errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
          console.log(errMsg);
          this.errorMessages.push({ description: 'Server error. Try later.' });
        });
  }


  signin(): void {
    this.oAuthService
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(this.model.username, this.model.password)
      .then(() => {
        this.authService.init();
        this.authService.scheduleRefresh();
        const redirect: string = this.authService.redirectUrl
          ? this.authService.redirectUrl
          : '/home';
        this.router.navigate([redirect]);
      })
      .catch((error: any) => {
        if (error.body !== '') {
          const body: any = error.json();

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
