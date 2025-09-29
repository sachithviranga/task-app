import { Routes } from '@angular/router';
import { inject } from '@angular/core';
import { AuthGuard } from  './core/guard/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'tasks',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadComponent: () => import('./auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'tasks',
    loadComponent: () => import('./layout/layout.component').then(m => m.LayoutComponent),
    canActivate: [() => inject(AuthGuard).canActivate()]
  },
  {
    path: '**',
    redirectTo: 'tasks'
  }
];
