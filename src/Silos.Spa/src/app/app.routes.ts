import { Routes } from '@angular/router';
import { HomeComponent } from './features/recordings/components/home/home.component';
import { LoginComponent } from './features/authentication/components/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { UserAccountComponent } from './features/authentication/components/user-account/user-account.component';
import { SiloSelectionComponent } from './features/recordings/components/silo-selection/silo-selection.component';
import { UserDetailsComponent } from './features/recordings/components/user-details/user-details.component';

export const APP_ROUTES: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'user-account',
    component: UserAccountComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
    pathMatch: 'full',
  },
  {
    path: 'silos',
    component: SiloSelectionComponent,
    canActivate: [AuthGuard],
    pathMatch: 'full',
  },
  {
    path: 'user-details',
    component: UserDetailsComponent,
    canActivate: [AuthGuard],
    pathMatch: 'full',
  },
  {
    path: '**',
    component: LoginComponent,
    canActivate: [AuthGuard],
    pathMatch: 'full',
  },
];
