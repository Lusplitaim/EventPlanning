import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ReplaySubject, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/user';
import { LoggedUserData } from '../models/loggedUserData';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | undefined>(1);
  currentUser$ = this.currentUserSource.asObservable();

  http = inject(HttpClient);

  login(model: any) {
    return this.http.post<LoggedUserData>(this.baseUrl + 'authentication/login', model).pipe(
      map((data) => {
        const user = data.user;
        if(user) {
          this.setCurrentUser(data);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<any>(this.baseUrl + 'authentication', model);
  }

  setCurrentUser(data: LoggedUserData) {
    //user.role = this.getDecodedToken(user.token).role;
    localStorage.setItem('user', JSON.stringify(data.user));
    localStorage.setItem('token', data.token);
    this.currentUserSource.next(data.user);
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.currentUserSource.next(undefined);
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]))
  }

  getToken(): string {
    return localStorage.getItem('token') ?? '';
  }

  isLoggedIn() {
    return localStorage.getItem('user') !== null;
  }

  getCurrentUser(): User {
    return JSON.parse(localStorage.getItem('user') ?? '') as User;
  }
}
