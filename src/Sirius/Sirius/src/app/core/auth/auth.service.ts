import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { interval } from 'rxjs/observable/interval';
import { timer } from 'rxjs/observable/timer';
import {User} from '../models/user';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {OAuthService} from 'angular-oauth2-oidc';


/**
 * Authentication service
 */
@Injectable()
export class AuthService {

  /**
   * auth data storage
   * @type {Storage}
   */
  public storage: Storage = localStorage;

  /**
   * Stores the URL so we can redirect after signing in.
   */
  public redirectUrl: string;

  /**
   * Behavior subjects of the user's status & data.
   */
  private signinStatus = new BehaviorSubject<boolean>(false);
  private user = new BehaviorSubject<User>(new User());

  /**
   * Scheduling of the refresh token.
   */
  private refreshSubscription: any;

  /**
   * Offset for the scheduling to avoid the inconsistency of data on the client.
   */
  private offsetSeconds: number = 30;

  constructor(
    private router: Router,
    private oAuthService: OAuthService
  ) { }

  /**
   * initializes service works
   */
  public init(): void {
    // Tells all the subscribers about the new status & data.
    this.signinStatus.next(true);
    this.user.next(this.getUser());
  }

  /**
   * Sign out fo app
   */
  public signout(): void {
    this.oAuthService.logOut(true);

    this.redirectUrl = null;

    // Tells all the subscribers about the new status & data.
    this.signinStatus.next(false);
    this.user.next(new User());

    // Unschedules the refresh token.
    this.unscheduleRefresh();
  }

  /**
   * Gets the access bearer token
   * @returns {string}
   */
  public getAccessToken(): string {
    const token: string = this.oAuthService.getAccessToken();
    return token;
  }

  /**
   * Checks is user is signed in
   * @returns {Observable<boolean>}
   */
  public isSignedIn(): Observable<boolean> {
    return this.signinStatus.asObservable();
  }

  /**
   * Allows to subscribe on user changes
   * @returns {Observable<User>}
   */
  public userChanged(): Observable<User> {
    return this.user.asObservable();
  }

  /**
   * Checks if user is in role
   * @param role Role name
   * @returns {boolean}
   */
  public isInRole(role: string): boolean {
    const user: User = this.getUser();
    const roles: string[] = user && typeof user.roles !== 'undefined' ? user.roles : [];
    return roles.indexOf(role) !== -1;
  }

  /**
   * Get current user
   * @returns {User}
   */
  public getUser(): User {
    const user: User = new User();
    if (this.oAuthService.hasValidAccessToken()) {
      const userInfo: any = this.oAuthService.getIdentityClaims();

      user.givenName = userInfo.given_name;
      user.userName = userInfo.name;
      user.roles = userInfo.role;
    }
    return user;
  }

  /**
   * Strategy for refresh token through a scheduler.
   * Will schedule a refresh at the appropriate time.
   */
  public scheduleRefresh(): void {
    const source: Observable<number> = interval(
      this.calcDelay(this.getAuthTime())
    );

    this.refreshSubscription = source.subscribe(() => {
      this.oAuthService.refreshToken()
        .then(() => {
          // Scheduler works.
        })
        .catch((error: any) => {
          this.handleRefreshTokenError();
        });
    });
  }

  /**
   * Case when the user comes back to the app after closing it.
   */
  public startupTokenRefresh(): void {
    if (this.oAuthService.hasValidAccessToken()) {
      const source: Observable<number> = timer(this.calcDelay(new Date().valueOf()));

      // Once the delay time from above is reached, gets a new access token and schedules additional refreshes.
      source.subscribe(() => {
        this.oAuthService.refreshToken()
          .then(() => {
            this.scheduleRefresh();
          })
          .catch((error: any) => {
            this.handleRefreshTokenError();
          });
      });
    }
  }

  /**
   * Unsubscribes from the scheduling of the refresh token.
   */
  private unscheduleRefresh(): void {
    if (this.refreshSubscription) {
      this.refreshSubscription.unsubscribe();
    }
  }

  /**
   * Handles errors on refresh token, like expiration.
   */
  private handleRefreshTokenError(): void {
    this.redirectUrl = this.router.url;

    // Tells all the subscribers about the new status & data.
    this.signinStatus.next(false);
    this.user.next(new User());

    // Unschedules the refresh token.
    this.unscheduleRefresh();

    // The user is forced to sign in again.
    this.router.navigate(['/account/signin']);
  }

  private calcDelay(time: number): number {
    const expiresAt: number = this.oAuthService.getAccessTokenExpiration();
    const delay: number = expiresAt - time - this.offsetSeconds * 1000;
    return delay > 0 ? delay : 0;
  }

  private getAuthTime(): number {
    return parseInt(this.storage.getItem('access_token_stored_at'), 10);
  }

}
