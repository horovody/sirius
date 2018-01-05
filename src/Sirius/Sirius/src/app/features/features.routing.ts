import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
const routes: Routes = [
  { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
  { path: 'home', loadChildren: './home/home.module#HomeModule' },
  { path: 'account', loadChildren: './account/account.module#AccountModule' },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class FeaturesRouting { }
