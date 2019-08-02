import { ModelBase } from './model-base';

export class AesCode extends ModelBase {
  code: string;
  longDescription: string;
  unitOfQuantity1: string;
  unitOfQuantity2: string;
  isExport: Boolean;

  aesEffectiveDates: AesEffectiveDate[];
}

export class AesEffectiveDate extends ModelBase {
  effectiveFrom: Date;
  effectiveTo: Date;
}
