import { GenericTableColumn } from 'angular-generics';
import { ClaimManufactureArticle } from '../models/claim-manufacture-article';

export class ClaimManufactureTable {
  private columns: GenericTableColumn[] = [
    new GenericTableColumn({
      id: 'id',
      display: 'ID',
      dataType: 'number',
      cell: (element: ClaimManufactureArticle) => `${element.id}`,
    }),
    new GenericTableColumn({
      id: 'actionIndicator',
      display: 'Action',
      dataType: 'string',
      cell: (element: ClaimManufactureArticle) => `${element.actionIndicator}`,
    }),
    new GenericTableColumn({
      id: 'importRulingNumber',
      display: 'Import Ruling Number',
      dataType: 'string',
      cell: (element: ClaimManufactureArticle) => `${element.importRulingNumber}`,
    }),
    new GenericTableColumn({
      id: 'hts',
      display: 'HTS',
      dataType: 'string',
      cell: (element: ClaimManufactureArticle) => `${element.hts}`,
    }),
    new GenericTableColumn({
      id: 'quantity',
      display: 'Quantity',
      dataType: 'number',
      cell: (element: ClaimManufactureArticle) => `${element.quantity}`,
    }),
    new GenericTableColumn({
      id: 'unitOfMeasure',
      display: 'Unit Of Measure',
      dataType: 'string',
      cell: (element: ClaimManufactureArticle) => `${element.unitOfMeasure}`,
    }),
    new GenericTableColumn({
      id: 'productionDate',
      display: 'Produced',
      dataType: 'date',
      cell: (element: ClaimManufactureArticle) => `${element.productionDate}`,
    }),
    new GenericTableColumn({
      id: 'factoryLocation',
      display: 'Factory Location',
      dataType: 'string',
      cell: (element: ClaimManufactureArticle) => `${element.factoryLocation}`,
    }),
    new GenericTableColumn({
      id: 'articleDescription',
      display: 'Description',
      dataType: 'string',
      cell: (element: ClaimManufactureArticle) => `${element.articleDescription}`,
    }),
    new GenericTableColumn({
      id: 'manufactureRulingNumber',
      display: 'Manufacture Ruling Number',
      dataType: 'string',
      cell: (element: ClaimManufactureArticle) => `${element.manufactureRulingNumber}`,
    }),
    new GenericTableColumn({
      id: 'mtin',
      display: 'MTIN',
      dataType: 'number',
      cell: (element: ClaimManufactureArticle) => `${element.mtin}`,
    }),
    new GenericTableColumn({
      id: 'relatedMTIN',
      display: 'Related MTIN',
      dataType: 'number',
      cell: (element: ClaimManufactureArticle) => `${element.relatedMTIN}`,
    }),
    new GenericTableColumn({
      id: 'relatedITIN',
      display: 'Related ITIN',
      dataType: 'number',
      cell: (element: ClaimManufactureArticle) => `${element.relatedITIN}`,
    })
  ];

  getColumns(): GenericTableColumn[] {
    // TODO: might make this return an observable so we can easily change columns
    return this.columns;
  }

}
