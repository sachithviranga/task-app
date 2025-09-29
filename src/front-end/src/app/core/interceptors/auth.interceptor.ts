import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Skip auth header for login endpoint
    if (req.url.includes('/auth/login')) {
      return next.handle(req);
    }

    const creds = this.authService.getCredentials();
    if (creds) {
      const authHeader = 'Basic ' + btoa(`${creds.username}:${creds.password}`);
      const cloned = req.clone({
        setHeaders: { Authorization: authHeader }
      });
      return next.handle(cloned);
    }
    return next.handle(req);
  }
}
