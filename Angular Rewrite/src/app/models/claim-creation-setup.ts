import { ModelBase } from './model-base';
import { CountryCode } from './codes';

export class ClaimRequestSetup extends ModelBase {

  // Import
  form7501s: string[];
  importers: string[];
  importRef1s: string[];
  ref3s: string[];
  ref4s: string[];
  cds: string[];

  // Export
  batches: string[];
  exporters: string[];
  countryofDesignations: CountryCode[];
  parts: string[];
  exportRef1s: string[];
  directIds: string[];

  constructor(init?: Partial<ClaimRequestSetup>) {
    super();
    Object.assign(this, init);
  }
}
