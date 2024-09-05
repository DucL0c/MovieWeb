import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './main/home/home.component';
import { MainComponent } from './main/main.component';
import { AppGroupComponent } from './main/system/app-group/app-group.component';
import { AppRoleComponent } from './main/system/app-role/app-role.component';
import { AppUserComponent } from './main/system/app-user/app-user.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'main',
    component: MainComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'home',
        component: HomeComponent,
      },
      {
        path: 'app-group',
        component: AppGroupComponent,
      },
      {
        path: 'app-role',
        component: AppRoleComponent,
      },
      {
        path: 'app-user',
        component: AppUserComponent,
      },
    ],
  },

  // { path: '**', redirectTo: '/login' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
