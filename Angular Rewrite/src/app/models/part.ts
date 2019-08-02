import { HtsCode } from './hts-code';
import { Client } from './client';
import { ModelBase } from './model-base';
import { SubPart } from './subpart';
import { AlternatePart } from './alternate-part';

export class Part extends ModelBase {
  number: string;
  description: string;
  factoryLocation: string;
  effectiveFrom: Date;
  effectiveTo: Date;

  client: Client;
  htsCode: HtsCode;

  SubParts: SubPart[];
  alternateParts: AlternatePart[];
}
