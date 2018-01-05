import {RouterModule, Routes} from '@angular/router';
import {PageNotFoundComponent} from './pages/404/page-not-found.component';
import {NgModule} from '@angular/core';
const routes: Routes = [
  {
    path: 'page-404',
    component: PageNotFoundComponent,
    data: {title: 'Page is not found'}
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
  ]
})
export class LayoutsRouting { }
