import { User } from './user';

export class ModelBase {
  id: Number;
  createdBy: User;
  dateCreated: Date;
  modifiedBy: User;
  dateModified: Date;
  rowVersion = Uint8Array;
}
