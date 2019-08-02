import { InventoryImport } from 'src/app/models/inventory-import';
import { formatDate } from '@angular/common';
import { GenericTableColumn } from 'angular-generics';

export class InventoryImportTable {
  private columns: GenericTableColumn[] = [
    new GenericTableColumn({
      id: 'id',
      display: 'Id',
      dataType: 'number',
      cell: (element: InventoryImport) => `${element.id}`,
      // footer: (element: InventoryImport) => 'Total',
    }),
    new GenericTableColumn({
      id: 'client.name',
      display: 'Client',
      cell: (element: InventoryImport) => `${element.client.name}`,
    }),
    new GenericTableColumn({
      id: 'receivedDate',
      display: 'Received',
      dataType: 'date',
      cell: (element: InventoryImport) => `${formatDate(element.receivedDate, 'yyyy-MM-dd', 'en')}`,
    }),
    new GenericTableColumn({
      id: 'htsCode.code',
      display: 'HTS',
      cell: (element: InventoryImport) => `${element.htsCode.code}`,
    }),
    new GenericTableColumn({
      id: 'availableQuantity',
      display: 'Quantity',
      dataType: 'number',
      cell: (element: InventoryImport) => `${element.availableQuantity}`,
      // footer: (element: InventoryImport) => '-',
    }),
    new GenericTableColumn({
      id: 'part.number',
      display: 'Part',
      cell: (element: InventoryImport) => `${element.part.number}`,
    }),
    new GenericTableColumn({
      id: 'factoryLocation',
      display: 'Location',
      cell: (element: InventoryImport) => `${element.factoryLocation}`,
    }),
    new GenericTableColumn({
      id: 'invoiceLineValue',
      display: 'Value',
      dataType: 'number',
      cell: (element: InventoryImport) => `${element.invoiceLineValue}`,
      // footer: (element: InventoryImport) => '-',
    }),
    new GenericTableColumn({
      id: 'inventoryStatus.name',
      display: 'Status',
      dataType: 'select',
      cell: (element: InventoryImport) => `${element.inventoryStatus.name}`,
    }),
  ];

  getColumns(): GenericTableColumn[] {
    // TODO: might make this return an observable so we can easily change columns
    return this.columns;
  }

}
