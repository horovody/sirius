import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from './home.component';
import {SharedModule} from '../../shared/shared.module';

export const routes: Routes = [
  { path: '', component: HomeComponent, data: {title: 'Home'} },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  declarations: [
    HomeComponent
  ],
  providers: [
  ]

})
export class HomeModule { }
