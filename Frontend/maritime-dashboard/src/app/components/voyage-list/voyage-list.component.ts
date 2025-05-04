import { CommonModule } from '@angular/common';
import { Component,OnInit } from '@angular/core';
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

@Component({
  selector: 'app-voyage-list',
  imports: [CommonModule,MatTableModule,MatIconModule,MatButtonModule],
  templateUrl: './voyage-list.component.html',
  styleUrl: './voyage-list.component.css'
})
export class VoyageListComponent implements OnInit {
  voyages: VoyageViewModel[] = [];
  displayedColumns: string[] = ['voyageDate', 'departurePortName', 'arrivalPortName', 'start', 'end', 'shipName', 'actions'];


constructor(
  private voyageService: VoyageService,
  private portService: PortService,
  private shipService: ShipService
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
  });
}


  deleteVoyage(id: string): void {
    this.voyageService.deleteVoyage(id).subscribe(() => {
      this.loadVoyages();
    });
  }

  editVoyage(voyage: any): void {
    console.log('Edit voyage:', voyage);
  }

}
