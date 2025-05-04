import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { PortService } from '../../services/port.service';
import { Port } from '../../../models/port.model';
import { Router } from '@angular/router';

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

  constructor(private portService: PortService, private router: Router) {}

  ngOnInit(): void {
    this.loadPorts();
  }

  loadPorts(): void {
    this.portService.getPorts().subscribe(data => {
      this.ports = data;
    });
  }

  deletePort(id: string): void {
    this.portService.deletePort(id).subscribe(() => {
      this.loadPorts();
    });
  }

  editPort(port: Port): void {
    console.log('Edit port:', port);
  }

}
