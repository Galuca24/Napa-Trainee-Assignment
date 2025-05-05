import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface VisitedCountryCount {
  country: string;
  count: number;
}

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  private baseUrl = 'https://localhost:7030/api/v1/statistics';

  constructor(private http: HttpClient) {}

  getVisitedCountriesCount(): Observable<VisitedCountryCount[]> {
    return this.http.get<VisitedCountryCount[]>(`${this.baseUrl}/visited-countries-detailed-last-year`);
  }


  fetchGeneric(url: string): Observable<any[]> {
    return this.http.get<any[]>(`https://localhost:7030${url}`);
  }


}
