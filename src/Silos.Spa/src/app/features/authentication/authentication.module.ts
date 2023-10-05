import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { UserAccountComponent } from './components/user-account/user-account.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [LoginComponent, UserAccountComponent],
  imports: [RouterModule, CommonModule, SharedModule],
})
export class AuthenticationModule {}
