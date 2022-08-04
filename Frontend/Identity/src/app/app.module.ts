import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { CoreModule } from './modules/core/core.module';
import { TokenInterceptor } from './modules/core/interceptors/token/token.interceptor';
import { LoadingInterceptor } from './modules/core/interceptors/loading/loading.interceptor';
import { ErrorHandlingInterceptor } from './modules/core/interceptors/error/error-handling.interceptor';

import { SharedModule } from './modules/shared/shared.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BaseSharedModule } from './modules/shared/sub-modules/base-shared/base-shared.module';
import { ConfigService } from 'core/services/config/config.service';

/* a head of compile functions */
const initializerConfigFn = (appConfig: ConfigService) => {
  return () => {
    return appConfig.loadAppConfig();
  };
};

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    BaseSharedModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlingInterceptor,
      multi: true
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initializerConfigFn,
      multi: true,
      deps: [ConfigService],
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
