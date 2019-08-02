import { InventoryImport } from './inventory-import';
import { ModelBase } from './model-base';

export class Merchandise extends ModelBase {
  lineNumber: string;
  description: string;
  adCvdNumber: string;
  grossWeight: number;
  manifestQuantity: number;
  netQuantity: number;
  netQuantityUnit: string;
  enteredValue: number;
  charges: number;
  relationship: string;
  adCvdRate: string;
  ircRate: string;
  visaNumber: string;
  dutyAndIrTax: number;

  import: InventoryImport;
}
