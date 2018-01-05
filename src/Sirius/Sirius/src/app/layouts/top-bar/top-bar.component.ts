import {Component, OnDestroy, OnInit} from '@angular/core';
import {AuthService} from '../../core/auth/auth.service';
import {User} from '../../core/models/user';
import {Observable} from 'rxjs/Observable';
import {Subscription} from 'rxjs/Subscription';
import {Router} from '@angular/router';

@Component({
  selector: 'sir-top-bar',
  templateUrl: './top-bar.component.html',
})
export class TopBarComponent implements OnInit, OnDestroy {
  signedIn: Observable<boolean>;
  user: User;
  subscription: Subscription;

  constructor(private authService: AuthService,
              private router: Router) {
  }

  ngOnInit() {
    this.signedIn = this.authService.isSignedIn();

    this.subscription =  this.authService.userChanged().subscribe(
      (user: User) => {
        this.user = user;
      });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  logout() {
    this.authService.signout();
    this.router.navigate(['/home']);
  }
}
