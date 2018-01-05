import {AuthService} from './auth.service';
import {AccountService} from './account.service';
import {AuthGuard} from './auth.guard';
import {RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {APP_INITIALIZER, Injector, NgModule} from '@angular/core';
import {OAuthModule} from 'angular-oauth2-oidc';
import {OAuthConfig} from './oauth.config';
import {AuthHttpInterceptor} from './auth.http.interceptor';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {AuthErrorInterceptor} from './auth.error.interceptor';

export function initOAuth(oAuthConfig: OAuthConfig): Function {
  return () => oAuthConfig.load();
}

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    RouterModule,
    OAuthModule.forRoot()
  ],
  exports: [
  ],
  providers: [
    AuthGuard,
    AccountService,
    AuthService,
    OAuthConfig,
    {
      provide: APP_INITIALIZER,
      useFactory: initOAuth,
      deps: [OAuthConfig],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthHttpInterceptor,
      multi: true,
      deps: [Injector],
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [  ]
})
export class AuthModule { }
