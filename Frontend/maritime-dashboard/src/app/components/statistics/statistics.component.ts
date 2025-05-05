import { Component } from '@angular/core';
import {
  Chart,
  registerables
} from 'chart.js';
import { NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StatisticsService, VisitedCountryCount } from '../../services/statistics.service';
import { STATISTIC_CONFIG } from '../../../models/chart.model';

@Component({
  selector: 'app-statistics',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule],
  templateUrl: './statistics.component.html'
})
export class StatisticsComponent {
  selectedCategory: string = '';
  selectedStatistic: string = '';
  chartTypes = ['pie', 'bar', 'doughnut'];
  selectedChartType: 'pie' | 'bar' | 'doughnut' = 'pie';
  categories: string[] = ['Countries', 'Ships', 'Voyages', 'Ports'];
  statisticsOptions: { [key: string]: string[] } = {
    Countries: ['Visited count'],
    Ships: ['Most used'],
    Voyages: ['Visits per month','Average duration per month'],
    Ports: ['Most arrivals']
  };

  showChart = false;
  chart: Chart | null = null;

  constructor(private statisticsService: StatisticsService) {
    Chart.register(...registerables);
  }

  generateChart() {
    if (this.chart) {
      this.chart.destroy();
    }

    const key = `${this.selectedCategory}|${this.selectedStatistic}`;
    const config = STATISTIC_CONFIG[key];

    if (!config) {
      console.warn('No chart configuration found for selection.');
      return;
    }

    this.showChart = true;

    this.statisticsService.fetchGeneric(config.url).subscribe((data: any[]) => {
      const labels = data.map(item => item[config.labelField]);
      const values = data.map(item => item[config.valueField]);
      const colors = this.generateColors(data.length);

      setTimeout(() => {
        this.chart = new Chart('chartCanvas', {
          type: this.selectedChartType,
          data: {
            labels,
            datasets: [
              {
                label: `${this.selectedCategory} - ${this.selectedStatistic}`,
                data: values,
                backgroundColor: this.selectedChartType === 'pie' ? colors : '#007bff'
              }
            ]
          },
          options: {
            responsive: true,
            plugins: {
              legend: {
                position: 'bottom'
              }
            }
          }
        });
      }, 100);
    });
  }

  onCategoryChange() {
    this.selectedStatistic = '';
    this.selectedChartType = 'pie';
    this.showChart = false;

    if (this.chart) {
      this.chart.destroy();
      this.chart = null;
    }
  }



  private generateColors(n: number): string[] {
    const colors: string[] = [];
    for (let i = 0; i < n; i++) {
      const hue = Math.floor(360 * i / n);
      colors.push(`hsl(${hue}, 70%, 50%)`);
    }
    return colors;
  }
}
