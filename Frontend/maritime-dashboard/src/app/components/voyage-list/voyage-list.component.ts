import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTable, MatTableModule } from '@angular/material/table';
import { VoyageService } from '../../services/voyage.service';
import { Voyage } from '../../../models/voyage.model';
import { VoyageViewModel } from '../../../models/voyage_view.model';
import { ShipService } from '../../services/ship.service';
import { lastValueFrom } from 'rxjs';
import { PortService } from '../../services/port.service';
import { Port } from '../../../models/port.model';
import { Ship } from '../../../models/ship.model';
import { VoyageEditComponent } from '../voyage-edit/voyage-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { MatNativeDateModule } from '@angular/material/core';
import { EnvironmentInjector } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-voyage-list',
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatNativeDateModule
  ],
  templateUrl: './voyage-list.component.html',
  styleUrl: './voyage-list.component.css'
})
export class VoyageListComponent implements OnInit {
  voyages: VoyageViewModel[] = [];
  displayedColumns: string[] = ['voyageDate', 'departurePortName', 'arrivalPortName', 'start', 'end', 'shipName', 'actions'];
  paginatedVoyages: VoyageViewModel[] = [];
  pageSize: number = 5;
  currentPage: number = 1;
  totalPages: number = 0;

  constructor(
    private dialog: MatDialog,
    private voyageService: VoyageService,
    private portService: PortService,
    private shipService: ShipService,
    private environmentInjector: EnvironmentInjector,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadVoyages();
  }

  loadVoyages(): void {
    this.voyageService.getVoyages().subscribe(async data => {
      const ports = await lastValueFrom(this.portService.getPorts());
      const ships = await lastValueFrom(this.shipService.getShips());

      this.voyages = data.map((v): VoyageViewModel => ({
        id: v.id,
        voyageDate: v.voyageDate,
        start: v.start,
        end: v.end,
        departurePortName: ports.find((p: Port) => p.id === v.departurePortId)?.name || 'Unknown',
        arrivalPortName: ports.find((p: Port) => p.id === v.arrivalPortId)?.name || 'Unknown',
        shipName: ships.find((s: Ship) => s.id === v.shipId)?.name || 'Unknown',
      }));

      this.totalPages = Math.ceil(this.voyages.length / this.pageSize);
      this.updatePaginatedVoyages();
    });
  }

  updatePaginatedVoyages(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedVoyages = this.voyages.slice(startIndex, endIndex);
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedVoyages();
    }
  }

  deleteVoyage(id: string): void {
    this.voyageService.deleteVoyage(id).subscribe({
      next: () => {
        this.toastr.success('Voyage deleted successfully!');
        this.loadVoyages();
      },
      error: err => {
        if (err.status === 500) {
          this.toastr.error('Voyage cannot be deleted due to a server-side error.');
        } else {
          this.toastr.error('Failed to delete voyage.');
        }
      }
    });
  }

  editVoyage(voyage: VoyageViewModel): void {
    Promise.all([
      lastValueFrom(this.portService.getPorts()),
      lastValueFrom(this.shipService.getShips()),
      lastValueFrom(this.voyageService.getVoyageById(voyage.id))
    ]).then(([ports, ships, fullVoyage]) => {
      const dialogRef = this.dialog.open(VoyageEditComponent, {
        width: '500px',
        data: {
          voyage: fullVoyage,
          ports,
          ships
        },
        injector: this.environmentInjector
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.voyageService.updateVoyage(result.id, result).subscribe({
            next: () => {
              this.toastr.success('Voyage updated successfully!');
              this.loadVoyages();
            },
            error: () => {
              this.toastr.error('Failed to update voyage.');
            }
          });
        }
      });
    }).catch(error => {
      console.error('Error loading data for edit dialog:', error);
    });
  }

  addVoyage(): void {
    Promise.all([
      lastValueFrom(this.portService.getPorts()),
      lastValueFrom(this.shipService.getShips())
    ]).then(([ports, ships]) => {
      const dialogRef = this.dialog.open(VoyageEditComponent, {
        width: '500px',
        data: {
          voyage: {} as Voyage,
          ports,
          ships
        },
        injector: this.environmentInjector
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.voyageService.createVoyage(result).subscribe({
            next: () => {
              this.toastr.success('Voyage created successfully!');
              this.loadVoyages();
            },
            error: () => {
              this.toastr.error('Failed to create voyage.');
            }
          });
        }
      });
    }).catch(error => {
      console.error('Error loading data for add dialog:', error);
    });
  }
}
