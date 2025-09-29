import { ApplicationConfig } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi, HTTP_INTERCEPTORS } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideStore } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { taskReducer } from './task/store/task/task.reducer';
import { taskStatusReducer } from './task/store/task-status/task-status.reducer';
import { TaskEffects } from './task/store/task/task.effects';
import { TaskStatusEffects } from './task/store/task-status/task-status.effects';
import { authReducer } from './auth/store/auth.reducer';
import { AuthEffects } from './auth/store/auth.effects';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    provideStore({
      tasks: taskReducer,
      taskStatuses: taskStatusReducer,
      auth: authReducer
    }),
    provideEffects([
      AuthEffects,
      TaskEffects,
      TaskStatusEffects
    ]),
    provideStoreDevtools({
      maxAge: 25,
      logOnly: false
    })
  ]
};
