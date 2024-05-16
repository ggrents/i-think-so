import { Injectable } from '@angular/core';
import { environment } from '../../../env/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUserRequest } from './models/user.request.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  registerUser(user: IUserRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/api/account/register`, user);
  }

  loginUser(user: IUserRequest): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/api/account/login`, user);
  }
}
