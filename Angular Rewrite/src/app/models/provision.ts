import { ProvisionRule } from './provision-rule';
import { ModelBase } from './model-base';

export class Provision extends ModelBase {
  name: string;
  description: string;
  provisionRules: ProvisionRule[];

  constructor(init?: Partial<Provision>) {
    super();
    Object.assign(this, init);
  }
}
