import {RouterModule, Routes} from '@angular/router';
import {SignupComponent} from './signup/signup.component';
import {SigninComponent} from './signin/signin.component';
import {NgModule} from '@angular/core';
const routes: Routes = [
  { path: 'signin', component: SigninComponent, data: {title: 'Sign In'} },
  { path: 'signup', component: SignupComponent, data: {title: 'Sign Up'} }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
