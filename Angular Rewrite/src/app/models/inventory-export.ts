import { MatchedPair } from './matched-pair';
import { Client } from './client';
import { HtsCode } from './hts-code';
import { InventoryStatusCode, CountryCode } from './codes';
import { Part } from './part';
import { Batch } from './batch';
import { ModelBase } from './model-base';

export class InventoryExport extends ModelBase {
  productionDate: Date;
  exportDate: Date;
  lineNumber: string;
  action: string;
  exportedQuantity: number;
  unitOfMeasure: string;
  availableQuantity: number;
  value: number;
  usEquivalentValue: number;
  carrier: string;
  factoryLocation: string;
  ref1: string;
  ref2OrDirectId: string;
  ref3: string;
  ref4: string;
  aesInternalNumber: string;
  naftaDuty: number;
  naftaRate: number;
  naftaTariff1: string;
  naftaTariff2: string;
  naftaTariff3: string;
  naftaEntry: string;
  naftaEntryDate: Date;

  client: Client;
  htsCode: HtsCode;
  scheduleB: HtsCode;
  inventoryStatus: InventoryStatusCode;
  countryOfDestination: CountryCode;
  part: Part;
  batch: Batch;
}
