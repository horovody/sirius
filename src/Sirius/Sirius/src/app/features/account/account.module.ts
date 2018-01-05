import {SignupComponent} from './signup/signup.component';
import {SigninComponent} from './signin/signin.component';
import {SharedModule} from '../../shared/shared.module';
import {AccountRoutingModule} from './account.routing';
import {NgModule} from '@angular/core';
@NgModule({
  imports: [
    AccountRoutingModule,
    SharedModule
  ],
  declarations: [
    SigninComponent,
    SignupComponent
  ]
})
export class AccountModule { }
