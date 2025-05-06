import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { ShipService } from '../../services/ship.service';
import { Ship } from '../../../models/ship.model';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ShipEditComponent } from '../ship-edit/ship-edit.component';
import { ToastrService } from 'ngx-toastr';

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
  paginatedShips: Ship[] = [];
  pageSize: number = 5;
  currentPage: number = 1;
  totalPages: number = 0;

  constructor(private dialog: MatDialog, private shipService: ShipService, private router: Router, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.loadShips();
  }

  loadShips(): void {
    this.shipService.getShips().subscribe(data => {
      this.ships = data;
      this.totalPages = Math.ceil(this.ships.length / this.pageSize);
      this.updatePaginatedShips();
    });
  }

  updatePaginatedShips(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedShips = this.ships.slice(startIndex, endIndex);
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedShips();
    }
  }

  deleteShip(id: string): void {
    this.shipService.deleteShip(id).subscribe({
      next: () => {
        this.toastr.success('Ship deleted successfully!');
        this.loadShips();
      },
      error: err => {
        if (err.status === 500) {
          this.toastr.error('Ship cannot be deleted because it is assigned to a voyage.');
        } else {
          this.toastr.error('Failed to delete port.');
        }
      }
    });
  }

  editShip(ship: Ship): void {
    const dialogRef = this.dialog.open(ShipEditComponent, {
      width: '400px',
      data: ship
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const updatedShip: Ship = { ...ship, ...result };
        this.shipService.updateShip(ship.id, updatedShip).subscribe({
          next: () => {
            this.toastr.success('Ship updated successfully!');
            this.loadShips();
          },
          error: err => {
            this.toastr.error('Failed to update ship!');
          }
        });
      }
    });
  }

  addShip(): void {
    const dialogRef = this.dialog.open(ShipEditComponent, {
      width: '400px',
      data: { name: '', maxSpeed: null }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.shipService.createShip(result).subscribe({
          next: () => {
            this.toastr.success('Ship created successfully!');
            this.loadShips();
          },
          error: err => {
            this.toastr.error('Failed to create ship ');
          }
        });
      }
    });
  }

  viewShipDetails(id: string): void {
    this.router.navigate(['/ships', id]);
  }
}
