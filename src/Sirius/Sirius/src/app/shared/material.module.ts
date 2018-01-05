import {NgModule} from '@angular/core';
import {
  MatButtonModule,
  MatCardModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatSidenavModule,
  MatTableModule,
  MatToolbarModule
} from '@angular/material';
const materialModules: any[] = [
  MatSidenavModule,
  MatToolbarModule,
  MatCardModule,
  MatListModule,
  MatInputModule,
  MatButtonModule,
  MatIconModule,
  MatTableModule
];

@NgModule({
  imports: materialModules,
  exports: materialModules
})

export class MaterialModule { }
