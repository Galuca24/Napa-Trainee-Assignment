import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ship } from '../../models/ship.model';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private baseUrl = 'https://localhost:7030/api/v1/Ships';

  constructor(private http: HttpClient) {}

  getShips(): Observable<Ship[]> {
    return this.http.get<Ship[]>(this.baseUrl);
  }

  getShipById(id: string): Observable<Ship> {
    return this.http.get<Ship>(`${this.baseUrl}/${id}`);
  }

  createShip(ship: Ship): Observable<Ship> {
    return this.http.post<Ship>(this.baseUrl, ship);
  }

  updateShip(id: string, ship: Ship): Observable<Ship> {
    return this.http.put<Ship>(`${this.baseUrl}/${id}`, ship);
  }

  deleteShip(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

}
