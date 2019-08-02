import { formatDate } from '@angular/common';
import { GenericTableColumn } from 'angular-generics';
import { ClaimExportArticle } from '../models/claim-export-articles';

export class ClaimExportTable {
  private columns: GenericTableColumn[] = [
    new GenericTableColumn({
      id: 'id',
      display: 'ID',
      dataType: 'number',
      cell: (element: ClaimExportArticle) => `${element.id}`,
    }),
    new GenericTableColumn({
      id: 'exportDestory',
      display: 'Export/Destory',
      dataType: 'string',
      cell: (element: ClaimExportArticle) => `${element.exportDestory}`,
    }),
    new GenericTableColumn({
      id: 'hts',
      display: 'HTS',
      dataType: 'string',
      cell: (element: ClaimExportArticle) => `${element.hts}`,
    }),
    new GenericTableColumn({
      id: 'quantity',
      display: 'Quantity',
      dataType: 'number',
      cell: (element: ClaimExportArticle) => `${element.quantity}`,
    }),
    new GenericTableColumn({
      id: 'unitOfMeasure',
      display: 'Unit Of Measure',
      dataType: 'string',
      cell: (element: ClaimExportArticle) => `${element.unitOfMeasure}`,
    }),
    new GenericTableColumn({
      id: 'exportDestroyDate',
      display: 'Export/Destroy Date',
      dataType: 'date',
      cell: (element: ClaimExportArticle) => `${element.exportDestroyDate}`,
    }),
    new GenericTableColumn({
      id: 'noticeOfIntent',
      display: 'Notice Of Intent',
      dataType: 'check',
      cell: (element: ClaimExportArticle) => `${element.noticeOfIntent}`,
    }),
    new GenericTableColumn({
      id: 'waiverToClaimRights',
      display: 'Waiver To Claim Rights',
      dataType: 'check',
      cell: (element: ClaimExportArticle) => `${element.waiverToClaimRights}`,
    }),
    new GenericTableColumn({
      id: 'countryOfUltimateDestination',
      display: 'Destination',
      dataType: 'string',
      cell: (element: ClaimExportArticle) => `${element.countryOfUltimateDestination}`,
    }),
    new GenericTableColumn({
      id: 'bolIndicator',
      display: 'BOL',
      dataType: 'check',
      cell: (element: ClaimExportArticle) => `${element.bolIndicator}`,
    }),
    new GenericTableColumn({
      id: 'carrierCode',
      display: 'Carrier Code',
      dataType: 'string',
      cell: (element: ClaimExportArticle) => `${element.carrierCode}`,
    }),
    new GenericTableColumn({
      id: 'articleDescription',
      display: 'Description',
      dataType: 'string',
      cell: (element: ClaimExportArticle) => `${element.articleDescription}`,
    }),
    new GenericTableColumn({
      id: 'uniqueIdentifier',
      display: 'Unique Identifier',
      dataType: 'string',
      cell: (element: ClaimExportArticle) => `${element.uniqueIdentifier}`,
    }),
    new GenericTableColumn({
      id: 'relatedITIN',
      display: 'Related ITIN',
      dataType: 'number',
      cell: (element: ClaimExportArticle) => `${element.relatedITIN}`,
    }),
    new GenericTableColumn({
      id: 'relatedMTIN',
      display: 'Related MTIN',
      dataType: 'number',
      cell: (element: ClaimExportArticle) => `${element.relatedMTIN}`
    })
  ];

  getColumns(): GenericTableColumn[] {
    // TODO: might make this return an observable so we can easily change columns
    return this.columns;
  }

}
