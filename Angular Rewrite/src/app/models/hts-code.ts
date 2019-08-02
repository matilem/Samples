import { NaicsCode, HiTechCode, StandardTradeCode, EndUseCode } from './codes';
import { ModelBase } from './model-base';

export class HtsCode extends ModelBase {
  code: string;
  shortDescription: string;
  longDescription: string;
  unitOfQuantity1: string;
  unitOfQuantity2: string;
  usda: number;
  isExport: boolean;
  isValidForAES: boolean;

  naicsCode: NaicsCode;
  hiTechCode: HiTechCode;
  standardTradeCode: StandardTradeCode;
  endUseCode: EndUseCode;
  htsEffectiveDates: HtsEffectiveDate[];
}

export class HtsEffectiveDate extends ModelBase {
  htsEffectiveDateId: number;
  effectiveFrom: Date;
  effectiveTo: Date;
}
