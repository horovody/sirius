import {ModuleWithProviders} from '@angular/core';
import {AuthGuard} from './core/auth/auth.guard';
import {RouterModule, Routes} from '@angular/router';
export const routes: Routes = [
  {path: '', redirectTo: 'dashboard', pathMatch: 'full', canActivate: [AuthGuard]},
  { path: '**', redirectTo: 'page-404' }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, { useHash: false });
