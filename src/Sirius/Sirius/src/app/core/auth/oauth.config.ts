import {environment} from '../../../environments/environment';
import {AuthConfig, OAuthService} from 'angular-oauth2-oidc';
import {Injectable} from '@angular/core';

export const oAuthConfig: AuthConfig = {
  clientId: 'AngularSPA',
  scope: 'openid offline_access WebAPI profile roles',
  oidc: false,
  issuer: environment.baseUrl,
  requireHttps: environment.isHttps
};

/**
 * angular-oauth2-oidc configuration.
 */
@Injectable()
export class OAuthConfig {

  constructor(private oAuthService: OAuthService) { }

  load(): Promise<object> {
    let url: string;

    this.oAuthService.configure(oAuthConfig);
    url = environment.baseUrl + '/.well-known/openid-configuration';

    // Defines the storage.
    this.oAuthService.setStorage(localStorage);

    // Loads Discovery Document.
    return this.oAuthService.loadDiscoveryDocument(url);
  }

}
