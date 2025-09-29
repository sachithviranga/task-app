import { createReducer, on } from '@ngrx/store';
import * as AuthActions from './auth.actions';

export interface AuthState {
  isAuthenticated: boolean;
  username: string;
  password: string;
  loading: boolean;
  error: string | null;
}

export const initialState: AuthState = {
  isAuthenticated: false,
  username: '',
  password: '',
  loading: false,
  error: null
};

export const authReducer = createReducer(
  initialState,

  on(AuthActions.login, state => ({
    ...state,
    loading: true,
    error: null
  })),

  on(AuthActions.loginSuccess, (state, { username, password }) => ({
    ...state,
    isAuthenticated: true,
    username,
    password,
    loading: false,
    error: null
  })),

  on(AuthActions.loginFailure, (state, { error }) => ({
    ...state,
    isAuthenticated: false,
    loading: false,
    error
  })),

  on(AuthActions.logout, state => ({
    ...state,
    loading: true
  })),

  on(AuthActions.logoutSuccess, state => ({
    ...state,
    isAuthenticated: false,
    username: '',
    password: '',
    loading: false,
    error: null
  })),

  on(AuthActions.checkAuth, state => ({
    ...state,
    loading: true
  })),

  on(AuthActions.checkAuthSuccess, (state, { username, password }) => ({
    ...state,
    isAuthenticated: true,
    username,
    password,
    loading: false,
    error: null
  })),

  on(AuthActions.checkAuthFailure, state => ({
    ...state,
    isAuthenticated: false,
    loading: false,
    error: null
  })),

  on(AuthActions.clearError, state => ({
    ...state,
    error: null
  }))
);

