export type ChartConfig = {
  url: string;
  labelField: string;
  valueField: string;
};

export const STATISTIC_CONFIG: { [key: string]: ChartConfig } = {
  'Countries|Visited count': {
    url: '/api/v1/Statistics/visited-countries-detailed-last-year',
    labelField: 'country',
    valueField: 'count'
  },
  'Ships|Most used': {
    url: '/api/v1/Statistics/ships/most-used',
    labelField: 'shipName',
    valueField: 'voyageCount'
  },
  'Voyages|Visits per month': {
    url: '/api/v1/Statistics/voyages/count-per-month',
    labelField: 'month',
    valueField: 'count'
  },
  'Voyages|Average duration per month': {
  url: '/api/v1/Statistics/voyages/average-duration-per-month',
  labelField: 'month',
  valueField: 'averageDuration'
  },

  'Ports|Most arrivals': {
  url: '/api/v1/Statistics/ports/most-arrivals',
  labelField: 'portName',
  valueField: 'arrivalCount'
  }


};

