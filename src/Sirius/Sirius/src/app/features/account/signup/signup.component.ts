import {OAuthService} from 'angular-oauth2-oidc';
import {Router} from '@angular/router';
import {AuthService} from '../../../core/auth/auth.service';
import {Component} from '@angular/core';
import {AccountService} from '../../../core/auth/account.service';
import {SigninComponentBase} from '../signin.component.base';
@Component({
  templateUrl: './signup.component.html'
})
export class SignupComponent extends SigninComponentBase {
  constructor(
    protected router: Router,
    protected oAuthService: OAuthService,
    protected authService: AuthService,
    protected accountService: AccountService) {
    super(router, oAuthService, authService);
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
}
