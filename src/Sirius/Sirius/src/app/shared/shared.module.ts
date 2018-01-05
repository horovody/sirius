import {NgModule} from '@angular/core';
import {MaterialModule} from './material.module';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';

const sharedModules: any[] = [
  CommonModule,
  FormsModule,
  HttpClientModule,
  MaterialModule
];

@NgModule({
  imports: sharedModules,
  exports: sharedModules
})

export class SharedModule { }
