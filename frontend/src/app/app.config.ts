import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { TUI_SANITIZER, TuiRootModule } from '@taiga-ui/core';
import { NgDompurifySanitizer } from '@tinkoff/ng-dompurify';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import routes from './app.routes';
import urlInterceptor from './interceptors/url.interceptor';

const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    provideRouter(routes),
    importProvidersFrom(TuiRootModule),
    { provide: TUI_SANITIZER, useClass: NgDompurifySanitizer },
    provideHttpClient(withInterceptors([urlInterceptor])),
  ],
};
export default appConfig;
