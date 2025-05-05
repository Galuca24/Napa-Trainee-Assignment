import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { PortService } from '../../services/port.service';
import { Port } from '../../../models/port.model';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { PortEditComponent } from '../port-edit/port-edit.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-port-list',
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './port-list.component.html',
  styleUrl: './port-list.component.css'
})
export class PortListComponent implements OnInit {
  ports: Port[] = [];
  displayedColumns: string[] = ['name', 'location', 'actions'];

  constructor(private dialog: MatDialog,private portService: PortService, private router: Router,  private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadPorts();
  }

  loadPorts(): void {
    this.portService.getPorts().subscribe(data => {
      this.ports = data;
    });
  }

  deletePort(id: string): void {
    this.portService.deletePort(id).subscribe({
      next: () => {
        this.toastr.success('Port deleted successfully!');
        this.loadPorts();
      },
      error: err => {
        if (err.status === 500) {
          this.toastr.error('Port cannot be deleted because it is assigned to a voyage.');
        } else {
          this.toastr.error('Failed to delete port.');
        }
      }
    });
  }



  editPort(port: Port): void {
    const dialogRef = this.dialog.open(PortEditComponent, {
      width: '400px',
      data: port
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const updatedPort: Port = { ...port, ...result };
        this.portService.updatePort(port.id, updatedPort).subscribe({
          next: () => {
            this.toastr.success('Port updated successfully!');
            this.loadPorts();
          },
          error: () => {
            this.toastr.error('Failed to update port.');
          }
        });
      }
    });
  }



addPort(): void {
  const dialogRef = this.dialog.open(PortEditComponent, {
    width: '400px',
    data: {
      name: '',
      location: ''
    }
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result) {
      this.portService.createPort(result).subscribe({
        next: () => {
          this.toastr.success('Port created successfully!');
          this.loadPorts();
        },
        error: err => {
          this.toastr.error('Failed to create port.');
        }
      });
    }
  });
}
}
