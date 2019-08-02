import { formatDate, formatCurrency } from '@angular/common';
import { GenericTableColumn } from 'angular-generics';
import { ClaimImport } from '../models/claim-import';

export class ClaimImportTable {
  private columns: GenericTableColumn[] = [
    new GenericTableColumn({
      id: 'id',
      display: 'ID',
      dataType: 'number',
      cell: (element: ClaimImport) => `${element.id}`,
    }),
    new GenericTableColumn({
      id: 'action',
      display: 'Action',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.action}`,
    }),
    new GenericTableColumn({
      id: 'entryFilerCode',
      display: 'Filer Code',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.entryFilerCode}`,
    }),
    new GenericTableColumn({
      id: 'entryNumber',
      display: 'Entry Number',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.entryNumber}`,
    }),
    new GenericTableColumn({
      id: 'cd',
      display: 'CD',
      dataType: 'check',
      cell: (element: ClaimImport) => `${element.cd}`,
    }),
    new GenericTableColumn({
      id: 'manufacturingRulingNumber',
      display: 'Ruling Number',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.manufacturingRulingNumber}`,
    }),
    new GenericTableColumn({
      id: 'basisOfClaim',
      display: 'Claim Basis',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.basisOfClaim}`,
    }),
    new GenericTableColumn({
      id: 'dateReceived',
      display: 'Received',
      dataType: 'date',
      cell: (element: ClaimImport) => `${formatDate(element.dateReceived, 'yyyy-MM-dd', 'en')}`,
    }),
    new GenericTableColumn({
      id: 'dateUsed',
      display: 'Used On',
      dataType: 'date',
      cell: (element: ClaimImport) => `${formatDate(element.dateUsed, 'yyyy-MM-dd', 'en')}`,
    }),
    new GenericTableColumn({
      id: 'importDate',
      display: 'Imported',
      dataType: 'date',
      cell: (element: ClaimImport) => `${formatDate(element.importDate, 'yyyy-MM-dd', 'en')}`,
    }),
    new GenericTableColumn({
      id: 'entryDate',
      display: 'Date Entered',
      dataType: 'date',
      cell: (element: ClaimImport) => `${formatDate(element.entryDate, 'yyyy-MM-dd', 'en')}`,
    }),
    new GenericTableColumn({
      id: 'hts',
      display: 'HTS',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.hts}`,
    }),
    new GenericTableColumn({
      id: 'articleDescription',
      display: 'Description',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.articleDescription}`,
    }),
    new GenericTableColumn({
      id: 'unitOfMeasure',
      display: 'Unit',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.unitOfMeasure}`,
    }),
    new GenericTableColumn({
      id: 'allowableQuantity',
      display: 'Allowable Quantity',
      dataType: 'number',
      cell: (element: ClaimImport) => `${element.allowableQuantity}`,
    }),
    new GenericTableColumn({
      id: 'goodsValuePerUnit',
      display: 'Unit Value',
      dataType: 'number',
      cell: (element: ClaimImport) => `${formatCurrency(element.goodsValuePerUnit, 'en-US', 'USD')}`,
    }),
    new GenericTableColumn({
      id: 'substitutedValuePerUnit',
      display: 'Unit Value Sub',
      dataType: 'number',
      cell: (element: ClaimImport) => `${formatCurrency(element.substitutedValuePerUnit, 'en-US', 'USD')}`,
    }),
    new GenericTableColumn({
      id: 'accountingClassCode',
      display: 'Accounting Code',
      dataType: 'string',
      cell: (element: ClaimImport) => `${element.accountingClassCode}`,
    }),
    new GenericTableColumn({
      id: 'claimAmount',
      display: 'Claim Amount',
      dataType: 'number',
      cell: (element: ClaimImport) => `${formatCurrency(element.claimAmount, 'en-US', 'USD')}`,
    }),
    new GenericTableColumn({
      id: 'calculatedAmount',
      display: 'Calculated Amount',
      dataType: 'number',
      cell: (element: ClaimImport) => `${formatCurrency(element.calculatedAmount, 'en-US', 'USD')}`,
    }),
    new GenericTableColumn({
      id: 'itin',
      display: 'ITIN',
      dataType: 'number',
      cell: (element: ClaimImport) => `${element.itin}`,
    }),
  ];

  getColumns(): GenericTableColumn[] {
    // TODO: might make this return an observable
    return this.columns;
  }

}
