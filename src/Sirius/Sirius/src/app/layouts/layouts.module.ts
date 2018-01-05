import {PageNotFoundComponent} from './pages/404/page-not-found.component';
import {TopBarComponent} from './top-bar/top-bar.component';
import {LayoutsRouting} from './layouts.routing';
import {RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {SharedModule} from '../shared/shared.module';
@NgModule({
  declarations: [
    TopBarComponent,
    PageNotFoundComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    LayoutsRouting
  ],
  exports: [
    TopBarComponent,
    PageNotFoundComponent
  ],
  providers: [  ],
  bootstrap: [  ]
})
export class LayoutsModule { }
