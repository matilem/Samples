export class User {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  isActive: boolean;
  dateCreated: Date;
  lastLogin: Date;
  rowVersion = Uint8Array;
}
