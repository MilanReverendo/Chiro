import { inject, Injectable, PLATFORM_ID, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserShortDto } from '../models/user-short-dto';
import { UserDto } from '../models/user-dto';
import { TokenResponseDto } from '../models/token-response-dto';
import { Observable, tap } from 'rxjs';
import { RefreshTokenRequestDto } from '../models/refresh-token-request-dto';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private platformId = inject(PLATFORM_ID);

  currentUser = signal<UserShortDto | null>(null);

  constructor() {
    if (isPlatformBrowser(this.platformId)) {
      this.rehydrateUser();
    }
  }

  private getStorage(key: string): string | null {
    if (!isPlatformBrowser(this.platformId)) return null;
    return localStorage.getItem(key);
  }

  private setStorage(key: string, value: string): void {
    if (!isPlatformBrowser(this.platformId)) return;
    localStorage.setItem(key, value);
  }

  private removeStorage(key: string): void {
    if (!isPlatformBrowser(this.platformId)) return;
    localStorage.removeItem(key);
  }

  private rehydrateUser() {
    const token = this.getStorage('at');
    if (!token) return;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const expiry = payload['exp'] * 1000;

      if (Date.now() >= expiry) {
        this.refreshToken().subscribe({
          next: res => {
            this.setStorage('at', res.accesToken);
            this.setStorage('rt', res.refreshToken);
            this.setUserFromToken(res.accesToken);
          },
          error: () => this.logout()
        });
        return;
      }

      this.setUserFromToken(token);
    } catch {
      this.logout();
    }
  }

  private setUserFromToken(token: string) {
    const payload = JSON.parse(atob(token.split('.')[1]));
    this.currentUser.set({
      id: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
      username: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
      isGroupLeader: false
    });
  }

  getAllUsers() {
    return this.http.get<UserShortDto[]>('/api/auth/all-users');
  }

  modifyUserDetails(user: UserShortDto): Observable<UserShortDto> {
    return this.http.put<UserShortDto>('api/auth/ModifyUserDetails', user);
  }

  login(credentials: UserDto) {
    return this.http.post<TokenResponseDto>('/api/auth/login', credentials).pipe(
      tap(response => this.handleAuthentication(response))
    );
  }

  logout() {
    this.removeStorage('at');
    this.removeStorage('rt');
    this.removeStorage('uid');

    this.currentUser.set(null);

    this.router.navigate(['/login']);
  }

  private handleAuthentication(response: TokenResponseDto) {
    this.setStorage('at', response.accesToken);
    this.setStorage('rt', response.refreshToken);

    const payload = JSON.parse(atob(response.accesToken.split('.')[1]));
    const userId = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

    this.setStorage('uid', userId);
    this.setUserFromToken(response.accesToken);
  }

  refreshToken() {
    const request: RefreshTokenRequestDto = {
      userId: this.getStorage('uid') || '',
      refreshToken: this.getStorage('rt') || ''
    };

    return this.http.post<TokenResponseDto>('/api/auth/refresh-token', request);
  }
}
