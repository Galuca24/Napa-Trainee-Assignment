import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Voyage } from '../../models/voyage.model';

@Injectable({
  providedIn: 'root'
})
export class VoyageService {
  private baseUrl = 'https://localhost:7030/api/v1/Voyages';
  constructor(private http:HttpClient) { }

 getVoyages():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }
  getVoyageById(id: string): Observable<Voyage> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }
  createVoyage(voyage: Voyage): Observable<Voyage> {
    return this.http.post<any>(this.baseUrl, voyage);
  }
  updateVoyage(id: string, voyage: Voyage): Observable<Voyage> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, voyage);
  }
  deleteVoyage(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
