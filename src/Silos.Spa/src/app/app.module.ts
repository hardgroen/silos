import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { RecordingsModule } from './features/recordings/recordings.module';
import { SharedModule } from './shared/shared.module';
import { AuthenticationModule } from './features/authentication/authentication.module';
import { APP_ROUTES } from './app.routes';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { ServerErrorInterceptor } from './core/interceptors/server-error.interceptor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoaderInterceptor } from './core/interceptors/loader.interceptor';
import { NavMenuComponent } from './shared/components/nav-menu/nav-menu.component';

@NgModule({
  declarations: [AppComponent, NavMenuComponent],
  imports: [
    SharedModule,
    CoreModule,
    RecordingsModule,
    AuthenticationModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(APP_ROUTES),
    NgbModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ServerErrorInterceptor,
      multi: true,
    },
  ],
  exports: [RouterModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
