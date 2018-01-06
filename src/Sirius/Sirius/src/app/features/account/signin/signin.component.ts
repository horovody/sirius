import {OAuthService} from 'angular-oauth2-oidc';
import {Router} from '@angular/router';
import {AuthService} from '../../../core/auth/auth.service';
import {Component} from '@angular/core';
import {SigninComponentBase} from '../signin.component.base';
@Component({
  templateUrl: './signin.component.html'
})
export class SigninComponent extends SigninComponentBase {
  constructor(
    protected router: Router,
    protected oAuthService: OAuthService,
    protected authService: AuthService) {
    super(router, oAuthService, authService);
  }
}
