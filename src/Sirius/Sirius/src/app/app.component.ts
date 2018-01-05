import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {OAuthService} from 'angular-oauth2-oidc';
import {AuthService} from './core/auth/auth.service';
import {Title} from '@angular/platform-browser';
import 'rxjs/add/operator/mergeMap';

@Component({
  selector: 'sir-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(
    private oAuthService: OAuthService,
    private authService: AuthService,
    private router: Router,
    private titleService: Title,
    private activatedRoute: ActivatedRoute
  ) {
    if (this.oAuthService.hasValidAccessToken()) {
      this.authService.init();
      this.authService.startupTokenRefresh();
    }
  }

  ngOnInit() {
    this.router.events
      .filter((event) => event instanceof NavigationEnd)
      .map(() => this.activatedRoute)
      .map((route) => {
        while (route.firstChild) route = route.firstChild;
        return route;
      })
      .filter((route) => route.outlet === 'primary')
      .mergeMap((route) => route.data)
      .subscribe((event) => this.titleService.setTitle(event['title']));
  }
}
