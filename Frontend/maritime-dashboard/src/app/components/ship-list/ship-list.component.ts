import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { ShipService } from '../../services/ship.service';
import { Ship } from '../../../models/ship.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ship-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './ship-list.component.html',
  styleUrls: ['./ship-list.component.css']
})
export class ShipListComponent implements OnInit {
  ships: Ship[] = [];
  displayedColumns: string[] = ['name', 'maxSpeed', 'actions'];

  constructor(private shipService: ShipService, private router: Router) {}

  ngOnInit(): void {
    this.loadShips();
  }

  loadShips(): void {
    this.shipService.getShips().subscribe(data => {
      this.ships = data;
    });
  }

  deleteShip(id: string): void {
    this.shipService.deleteShip(id).subscribe(() => {
      this.loadShips();
    });
  }

  editShip(ship: Ship): void {
    console.log('Edit ship:', ship);
  }
}
