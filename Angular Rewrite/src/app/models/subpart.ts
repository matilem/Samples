import { Part } from './part';
import { ModelBase } from './model-base';

export class SubPart extends ModelBase{
  parentPart: Part;
  childPart: Part;
  quantity: number;
  effectiveFrom: Date;
  effectiveTo: Date;
}
