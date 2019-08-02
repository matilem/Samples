import { ModelBase } from './model-base';

export class ClaimExportArticle extends ModelBase {
  id: number;
  exportDestory: string;
  hts: string;
  quantity: number;
  unitOfMeasure: string;
  exportDestroyDate: Date;
  noticeOfIntent: boolean;
  waiverToClaimRights: boolean;
  countryOfUltimateDestination: string;
  bolIndicator: boolean;
  carrierCode: string;
  articleDescription: string;
  uniqueIdentifier: string;
  relatedITIN: number;
  relatedMTIN: number;
}
