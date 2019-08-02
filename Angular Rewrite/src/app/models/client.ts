import { ModelBase } from './model-base';

export class Client extends ModelBase {
  irsNumber: string;
  name: string;

  constructor(init?: Partial<Client>) {
    super();
    Object.assign(this, init);
  }
}
