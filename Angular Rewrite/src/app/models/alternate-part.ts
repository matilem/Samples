import { Part } from './part';
import { ModelBase } from './model-base';

export class AlternatePart extends ModelBase {
  part: Part;
  alternateNumber: string;
  effectiveFrom: Date;
  effectiveTo: Date;
}
