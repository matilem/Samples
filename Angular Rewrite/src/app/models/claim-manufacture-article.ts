import { ModelBase } from './model-base';

export class ClaimManufactureArticle extends ModelBase {
  id: number;
  actionIndicator: string;
  importRulingNumber: string;
  hts: string;
  quantity: number;
  unitOfMeasure: string;
  productionDate: Date;
  factoryLocation: string;
  articleDescription: string;
  manufactureRulingNumber: string;
  mtin: number;
  relatedMTIN: number;
  relatedITIN: number;
}
