import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Survey } from './models/survey.interface';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAllSurveys(): Observable<Survey[]> {
    return this.http.get<Survey[]>(`${this.baseUrl}/surveys`);
  }

  getSurveyById(id: string): Observable<Survey> {
    return this.http.get<Survey>(`${this.baseUrl}/surveys/${id}`);
  }

  createSurvey(survey: Survey): Observable<Survey> {
    return this.http.post<Survey>(`${this.baseUrl}/surveys`, survey);
  }

  updateSurvey(id: string, survey: Survey): Observable<Survey> {
    return this.http.put<Survey>(`${this.baseUrl}/surveys/${id}`, survey);
  }

  deleteSurvey(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/surveys/${id}`);
  }
}
