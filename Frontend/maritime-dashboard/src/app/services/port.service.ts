import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Port } from '../../models/port.model';
@Injectable({
  providedIn: 'root'
})
export class PortService {
  private baseUrl = 'https://localhost:7030/api/v1/Ports';

  constructor(private http:HttpClient) { }

  getPorts(): Observable<Port[]> {
    return this.http.get<Port[]>(this.baseUrl);
  }

  getPortById(id: string): Observable<Port> {
    return this.http.get<Port>(`${this.baseUrl}/${id}`);
  }

  createPort(port: Port): Observable<Port> {
    return this.http.post<Port>(this.baseUrl, port);
  }

  updatePort(id: string, port: Port): Observable<Port> {
    return this.http.put<Port>(`${this.baseUrl}/${id}`, port);
  }

  deletePort(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
