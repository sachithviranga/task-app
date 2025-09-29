import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CryptoUtils } from '../utils/crypto.utils';
import { AuthApiService, LoginResponse } from './auth-api.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private username = '';
  private password = '';
  private readonly STORAGE_KEY = 'auth_credentials';

  constructor(private authApiService: AuthApiService) {
    this.restore();
  }

  login(username: string, password: string): Observable<LoginResponse> {
    return this.authApiService.login({ username, password });
  }

  setCredentials(username: string, password: string): void {
    this.username = username;
    this.password = password;
    const raw = JSON.stringify({ username, password });
    const encrypted = CryptoUtils.encrypt(raw);
    localStorage.setItem(this.STORAGE_KEY, encrypted);
  }

  logout(): void {
    this.username = '';
    this.password = '';
    localStorage.removeItem(this.STORAGE_KEY);
  }

  getCredentials(): { username: string; password: string } | null {
    if (this.username && this.password) {
      return { username: this.username, password: this.password };
    }
    return null;
  }

  isLoggedIn(): boolean {
    return !!this.getCredentials();
  }

  private restore(): void {
    const encrypted = localStorage.getItem(this.STORAGE_KEY);
    if (encrypted) {
      try {
        const decrypted = CryptoUtils.decrypt(encrypted);
        const creds = JSON.parse(decrypted);
        this.username = creds.username;
        this.password = creds.password;
      } catch (err) {
        console.warn('Failed to decrypt credentials');
        this.logout();
      }
    }
  }
}
