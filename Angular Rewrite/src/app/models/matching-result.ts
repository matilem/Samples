import { ModelBase } from './model-base';

export class MatchingResult extends ModelBase {
    importQuantity: number;
    totalImportsValue: number;
    totalDutyPaid: number;
    claimableDuty: number;
    notClaimableDuty: number;
    exportQuantity: number;
    totalExportsValue: number;
    utilizedExportsValue: number;
}