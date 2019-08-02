import { InventoryImport } from './inventory-import';
import { ModelBase } from './model-base';
import { Claim } from './claim';
import { InventoryExport } from './inventory-export';

export class MatchedPair extends ModelBase {
  import: InventoryImport;
  export: InventoryExport;
  claim: Claim;

  importQuantity: number;
  importValue: number;

  exportQuantity: number;
  exportValue: number;

  dutyPaid: number;
}
