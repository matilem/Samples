import { InventoryImport } from './inventory-import';
import { Client } from './client';
import { ModelBase } from './model-base';

export class Sale extends ModelBase {
  seller: Client;
  buyer: Client;
  import: InventoryImport;
  invoiceNumber: string;
  quantity: number;
  salePrice: number;
  saleDate: Date;
}
