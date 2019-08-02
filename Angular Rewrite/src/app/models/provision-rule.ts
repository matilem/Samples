import { ModelBase } from './model-base';

export class ProvisionRule extends ModelBase {
  ruleTitle: string;
  ruleText: string;

  constructor(init?: Partial<ProvisionRule>) {
    super();
    Object.assign(this, init);
  }
}
