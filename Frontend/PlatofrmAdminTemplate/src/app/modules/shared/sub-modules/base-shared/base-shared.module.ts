import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { MomentModule } from 'ngx-moment';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    TranslateModule,
    MomentModule,
  ],
  exports: [
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    TranslateModule,
    MomentModule,
  ]
})
export class BaseSharedModule { }
