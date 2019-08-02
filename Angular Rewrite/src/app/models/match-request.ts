import { Client } from './client';
import { Provision } from './provision';
import { Form7501 } from './form-7501';
import { Batch } from './batch';
import { CountryCode } from './codes';
import { Part } from './part';

export class DateRange {
  from: Date;
  to: Date;

  constructor(init?: Partial<DateRange>) {
    Object.assign(this, init);
  }
}

export class NumberRange {
  from: number;
  to: number;

  constructor(init?: Partial<NumberRange>) {
    Object.assign(this, init);
  }
}
export class MatchRequest {
  clientId: number;
  importerId: number;
  exporterId: number;
  provisionId: number;
  form7501Id: number;

  exportBatchId: number;
  destinationCountryId: number;
  exportPartId: number;

  // internalId: string;
  matchBy: string;
  importsPriority: string;

  exportType: string;
  exportPriority: string;

  importRef1: string;
  importDirectId: string;
  importRef3: string;
  importRef4: string;

  cd: string;

  exportRef1: string;
  exportDirectId: string;
  exportRef3: string;
  exportRef4: string;

  skipRef2DirectId: boolean;
  oilSpillTax: boolean;
  alternativesApply: boolean;
  multiLevelBom: boolean;
  nafta: boolean;
  citrus: boolean;
  allowImportDateEqualExportDate: boolean;
  domesticFreeApply: boolean;

  importReceivedRange: DateRange;
  importUsedRange: DateRange;
  importImportedRange: DateRange;
  importExportEntryRange: DateRange;

  exportDateRange: DateRange;
  exportProducedRange: DateRange;

  marketValueDifferenceRange: NumberRange;

  constructor(init?: Partial<MatchRequest>) {
    Object.assign(this, init);
  }
}
