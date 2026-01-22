import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7252/api/Auth';

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  // LOGIN
  login(credentials: { username: string; password: string }) {
    return this.http.post<any>(`${this.apiUrl}/login`, credentials);
  }

  // STORE TOKEN
  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  // GET TOKEN
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // CHECK LOGIN STATUS
  isLoggedIn(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const payload = JSON.parse(atob(token.split('.')[1]));
    const expiry = payload.exp * 1000;

    return Date.now() < expiry;
  }

  // GET USER ROLE
  getUserRole(): string {
    const token = this.getToken();
    if (!token) return '';

    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload['role'];
  }

  // LOGOUT
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
