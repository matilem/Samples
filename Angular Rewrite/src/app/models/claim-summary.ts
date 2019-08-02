import { ModelBase } from './model-base';

export class ClaimSummary extends ModelBase {
  internalId: string;
  entryNumber: string;
  filingPort: string;
  filingDate: Date;
  filerCode: string;
  provision: string;
  basisOfClaim: string;

  manufactureRulingNumber: string;
  exportIndicator: string;
  accountingMethodCode: string;
  acceleratedPaymentRequired: string;
  oneTimeWaiver: string;
  waiverPriorNotice: string;
  cidcr: string;
  electronicPetroleumCertification: string;
  oilSpillTaxCertification: string;
  claimantId: string;
  claimantIdSuffix: string;
  designatedNotifyParty: string;
  designatedNotifyPartyFEIN: string;
  designatedNotifyPartyPart: string;
  substitutedUnusedWineCertification: string;
  certificationForValuationOfDestroyedMerchandise: string;
  billOfMaterialsFormulaCertification: string;
  summaryFilingAction: string;

  preparerInitials: string;
  contactName: string;
  contactPhone: string;
  contactExt: string;
}
