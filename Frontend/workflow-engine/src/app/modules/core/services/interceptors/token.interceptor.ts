import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from '../storage/storage.service';
import { finalize } from 'rxjs/operators';
import { LoaderService } from 'shared/services/loading/loading.service';

@Injectable({
  providedIn: 'root'
})

/**
 * Token Interceptor
 * An Interceptor for add auth token to the header of each http
 */
export class TokenInterceptor implements HttpInterceptor {
  private token = '';
  constructor(private storage: StorageService, public loaderService: LoaderService) { }
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.loaderService.show();
    this.token = this.storage.getTokenFromSessionStorage();
    request = request.clone({
      setHeaders: {
        Authorization: 'Bearer ' + this.token
      }
    });
    return next.handle(request).pipe(finalize(() => this.loaderService.hide()));
  }
}
