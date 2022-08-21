import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { StorageService } from 'core/services/storage/storage.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  private token: string | null = null;
  private language: string | null = null;
  constructor(private storage: StorageService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    this.token = this.storage.getTokenFromSessionStorage();
    this.language = this.storage.getItem('currentLang');
    request = request.clone({
      setHeaders: {
        'Authorization': `Bearer ${this.token ? this.token : ''}`,
        'Cache-Control': 'no-store, must-revalidate',
        'Pragma': 'no-cache',
        'Expires': '0',
        'Accept-Language': `${this.language}`
      }
    });

    if (request.url.includes(environment.HOST_API)) {
      return next.handle(request);
    }

    return next.handle(request);
  }
}
