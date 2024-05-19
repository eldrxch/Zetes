import { Injectable } from '@angular/core';
import { Customer } from './models/customer';
import { Observable } from 'rxjs';  
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { emit } from 'process';
import { User } from './models/user';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  authUrl = environment.authUrl;
  usersUrl = environment.apiUrl + '/users';

  constructor(private http:HttpClient) { }

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.authUrl}/login`, {email, password});
  }

  register(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.authUrl}/register`, {email, password});
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.usersUrl}`);
  }

  deleteUser(user: User): Observable<any> {
    return this.http.delete(`${this.usersUrl}/${user.userId}`);
  }
}
