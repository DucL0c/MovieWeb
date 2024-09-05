import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {
  HTTP_INTERCEPTORS,
  HttpClient,
  provideHttpClient,
} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { LoginComponent } from './login/login.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from './core/material/material.module';
import { SharedModule } from './core/_modules/shared.module';
import { MainComponent } from './main/main.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './main/home/home.component';
import { DatePipe } from '@angular/common';
import { AppGroupComponent } from './main/system/app-group/app-group.component';
import { AppRoleComponent } from './main/system/app-role/app-role.component';
import { AppUserComponent } from './main/system/app-user/app-user.component';
import { UserRoleModule } from './core/common/userRole.pipe';
import { UserPipeModule } from './core/common/userPipe.pipe';
import { ErrorInterceptor } from './core/_interceptors/error.interceptor';
import { JwtInterceptor } from './core/_interceptors/jwt.interceptor';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MainComponent,
    HomeComponent,
    AppGroupComponent,
    AppRoleComponent,
    AppUserComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    MaterialModule,
    SharedModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    UserRoleModule,
    UserPipeModule,
  ],
  providers: [
    AuthGuard,
    provideAnimationsAsync(),
    provideHttpClient(),
    DatePipe,
    // { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    // { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
