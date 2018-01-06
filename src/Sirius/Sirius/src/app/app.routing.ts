import {ModuleWithProviders} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
export const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  { path: '**', redirectTo: 'page-404' }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, { useHash: false });
