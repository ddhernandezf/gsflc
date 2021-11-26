export class BalanceFilter {
  vehicles: string[];
  services: string[];
  expenses: string[];
  dateRange: boolean;
  startDate: Date;
  endDate: Date;
  monthAndYear: boolean;
  month: number;
  year: number;
  docType: string;
}
