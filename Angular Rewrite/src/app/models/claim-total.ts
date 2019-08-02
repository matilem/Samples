import { ModelBase } from './model-base';
import { ClaimAccountingClassCodes } from './claim-accounting-class-code';

export class ClaimTotal extends ModelBase {
  grandTotalDutyAmount: number;
  grandTotalUserFeeAmount: number;
  grandTotalIRTaxAmount: number;
}
