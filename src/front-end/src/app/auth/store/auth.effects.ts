import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Router } from '@angular/router';
import { catchError, map, mergeMap, of, tap } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import {
  login, loginFailure, loginSuccess,
  logout, logoutSuccess,
  checkAuth, checkAuthFailure, checkAuthSuccess
} from './auth.actions';

@Injectable()
export class AuthEffects {
  login$;
  loginSuccess$;
  logout$;
  logoutSuccess$;
  checkAuth$;

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private router: Router
  ) {
    this.login$ = createEffect(() =>
      this.actions$.pipe(
        ofType(login),
        mergeMap(({ username, password }) =>
          this.authService.login(username, password).pipe(
            map((response) => {
              if (response.isValid) {
                this.authService.setCredentials(username, password);
                return loginSuccess({ username, password });
              } else {
                return loginFailure({ error: response.message });
              }
            }),
            catchError(error =>
              of(loginFailure({ error: error.message || 'Login failed' }))
            )
          )
        )
      )
    );

    this.loginSuccess$ = createEffect(() =>
      this.actions$.pipe(
        ofType(loginSuccess),
        tap(() => {
          this.router.navigate(['/tasks']);
        })
      ),
      { dispatch: false }
    );

    this.logout$ = createEffect(() =>
      this.actions$.pipe(
        ofType(logout),
        mergeMap(() => {
          this.authService.logout();
          return of(logoutSuccess());
        }),
        catchError(() => of(loginFailure({ error: 'Logout failed' })))
      )
    );

    this.logoutSuccess$ = createEffect(() =>
      this.actions$.pipe(
        ofType(logoutSuccess),
        tap(() => {
          this.router.navigate(['/login']);
        })
      ),
      { dispatch: false }
    );

    this.checkAuth$ = createEffect(() =>
      this.actions$.pipe(
        ofType(checkAuth),
        mergeMap(() => {
          const credentials = this.authService.getCredentials();
          if (credentials && this.authService.isLoggedIn()) {
            return this.authService.login(credentials.username, credentials.password).pipe(
              map((response) => {
                if (response.isValid) {
                  return checkAuthSuccess({
                    username: credentials.username,
                    password: credentials.password
                  });
                } else {
                  this.authService.logout();
                  return checkAuthFailure();
                }
              }),
              catchError(() => {
                this.authService.logout();
                return of(checkAuthFailure());
              })
            );
          } else {
            return of(checkAuthFailure());
          }
        }),
        catchError(() => of(checkAuthFailure()))
      )
    );
  }
}
