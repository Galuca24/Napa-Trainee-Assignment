import { Routes } from '@angular/router';
import { ShipListComponent } from './components/ship-list/ship-list.component';
import { PortListComponent } from './components/port-list/port-list.component';
import { VoyageListComponent } from './components/voyage-list/voyage-list.component';
import { StatisticsComponent } from './components/statistics/statistics.component';

export const routes: Routes = [
  { path: 'ships', component: ShipListComponent },
  { path: 'ports', component: PortListComponent },
  { path: 'voyages', component: VoyageListComponent },
  { path: 'statistics', component: StatisticsComponent },
  { path: '', redirectTo: 'ships', pathMatch: 'full' }
];
