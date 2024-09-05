import { NgModule } from '@angular/core';
import { NgProgressModule } from 'ngx-progressbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { HttpClientModule } from '@angular/common/http';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { LayoutModule } from '@angular/cdk/layout';
import { NgProgressHttpModule } from 'ngx-progressbar/http';
import { NgxSpinnerModule } from 'ngx-spinner';
@NgModule({
  declarations: [],
  imports: [
    NgProgressModule.withConfig({ spinnerPosition: 'right', color: '#red' }),
    MatSidenavModule,
    LayoutModule,
    NgProgressHttpModule,
    FlexLayoutModule,
    FormsModule,
    RouterModule,
    CommonModule,
    HttpClientModule,
    NgxSpinnerModule,
  ],
  exports: [
    NgProgressModule,
    MatSidenavModule,
    LayoutModule,
    NgProgressHttpModule,
    FlexLayoutModule,
    FormsModule,
    RouterModule,
    CommonModule,
    HttpClientModule,
    NgxSpinnerModule,
  ],
})
export class SharedModule {}
