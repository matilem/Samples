import { GenericTableColumn } from 'angular-generics';
import { MatchingResult } from '../models/matching-result';

export class MatchingResultTable {
  private columns: GenericTableColumn[] = [
    new GenericTableColumn({
      id: 'importQuantity',
      display: 'Import Quantity',
      dataType: 'number',
      cell: (element: MatchingResult) => `${element.importQuantity}`,
    }),
    new GenericTableColumn({
      id: 'totalImportsValue',
      display: 'Total Imports Value',
      dataType: 'number',
      cell: (element: MatchingResult) => `${element.totalImportsValue}`,
    }),
    new GenericTableColumn({
        id: 'totalDutyPaid',
        display: 'Total Duty Paid',
        dataType: 'number',
        cell: (element: MatchingResult) => `${element.totalDutyPaid}`,
      }),
    new GenericTableColumn({
        id: 'claimableDuty',
        display: 'Claimable Duty',
        dataType: 'number',
        cell: (element: MatchingResult) => `${element.claimableDuty}`,
      }),
    new GenericTableColumn({
        id: 'notClaimableDuty',
        display: 'Not Claimable Duty',
        dataType: 'number',
        cell: (element: MatchingResult) => `${element.notClaimableDuty}`,
      }),
    new GenericTableColumn({
        id: 'exportQuantity',
        display: 'Export Quantity',
        dataType: 'number',
        cell: (element: MatchingResult) => `${element.exportQuantity}`,
      }),
    new GenericTableColumn({
        id: 'totalExportsValue',
        display: 'Total Exports Value',
        dataType: 'number',
        cell: (element: MatchingResult) => `${element.totalExportsValue}`,
      }),
    new GenericTableColumn({
        id: 'utilizedExportsValue',
        display: 'Utilized Exports Value',
        dataType: 'number',
        cell: (element: MatchingResult) => `${element.utilizedExportsValue}`,
      }),
  ];

  getColumns(): GenericTableColumn[] {
    return this.columns;
  }
}
