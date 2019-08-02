import { Client } from './client';
import { HtsCode } from './hts-code';
import { Part } from './part';
import { ModelBase } from './model-base';
import { InventoryStatusCode } from './codes';
import { Batch } from './batch';

export class InventoryImport extends ModelBase {
  availableQuantity: number;
  invoiceDocumentedQuantity: number;
  invoiceReceivedQuantity: number;
  unitOfMeasure: string;
  invoiceLineValue: number;
  receivedDate: Date;
  usedInDate: Date;
  liquidationDate: Date;
  merchandiseProcessingFee: number;
  harborMaintenanceTax: number;
  factoryLocation: string;

  ref1: string;
  ref2OrDirectId: string;
  ref3: string;
  ref4: string;

  unitSpecificRate: number;
  adValoremRate: number;

  federalOilSpillTaxPaid: number;
  federalOilSpillTaxRate: number;
  federalOilSpillTaxQuantity: number;
  federalOilSpillTaxUnits: string;
  federalOilSpillTax: number;

  domesticExciseTax: number;
  tax: number;
  taxConvention: string;
  certificateOfDelivery: string;
  watches: string;

  quantity2: number;
  quantity3: number;
  quantity4: number;
  units2: string;
  units3: string;
  units4: string;
  rate2: number;
  rate3: number;
  rate4: number;
  value2: number;
  value3: number;
  value4: number;

  client: Client;
  htsCode: HtsCode;
  part: Part;
  inventoryStatus: InventoryStatusCode;
  batch: Batch;
}
