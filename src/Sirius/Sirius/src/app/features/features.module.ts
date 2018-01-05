import {FeaturesRouting} from './features.routing';
import {SharedModule} from '../shared/shared.module';
import {RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    FeaturesRouting
  ]
})
export class FeaturesModule { }
