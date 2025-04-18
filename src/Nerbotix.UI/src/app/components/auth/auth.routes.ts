import {Routes} from '@angular/router';
import {LoginComponent} from './login/login.component';
import {inject} from '@angular/core';
import {AuthService} from '../../services/auth/auth.service';
import {ForgotComponent} from './forgot/forgot.component';
import {RegisterComponent} from './register/register.component';
import {CreateCompanyComponent} from './create-company/create-company.component';

export const AUTH_ROUTES: Routes = [
  { path: 'login', component: LoginComponent, title: 'Login' },
  { path: 'forgot', component: ForgotComponent, title: 'Forgot Password' },
  { path: 'register', component: RegisterComponent, title: 'Register' },
  { path: 'new-company', component: CreateCompanyComponent, title: 'New Company' },
  { path: 'logout',
    redirectTo: () => {
      const authService = inject(AuthService);
      authService.logout();
      return '/';
    }
  }
];
