export class ClaimImport {
  id: number;
  action: string;
  entryFilerCode: string;
  entryNumber: string;
  cd: boolean;
  manufacturingRulingNumber: string;
  basisOfClaim: string;
  dateReceived: Date;
  dateUsed: Date;
  importDate: Date;
  entryDate: Date;
  hts: string;
  articleDescription: string;
  unitOfMeasure: string;
  allowableQuantity: number;
  goodsValuePerUnit: number;
  substitutedValuePerUnit: number;
  accountingClassCode: string;
  claimAmount: number;
  calculatedAmount: number;
  itin: number;
}
