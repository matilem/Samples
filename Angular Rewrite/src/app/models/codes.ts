import { ModelBase } from './model-base';

class CodeBase extends ModelBase {
  code: string;
  description: string;
}

export class BondTypeCode extends CodeBase { }

export class StandardTradeCode extends CodeBase { }

export class NaicsCode extends CodeBase { }

export class EntryTypeCode extends CodeBase { }

export class HiTechCode extends CodeBase { }

export class TransportationCode extends CodeBase {
  x12Code: string;
}

export class StandardClassificationCode extends CodeBase {
  isExport: boolean;
}

export class EndUseCode extends CodeBase {
  isExport: boolean;
}

export class PortCode extends ModelBase {
  name: string;
  type: string;
  port: string;
  countryCode: CountryCode;
}

export class CountryCode {
  name: string;
  code: string;
  isoCode: string;
}

export class InventoryStatusCode extends ModelBase {
  name: string;
  description: string;
}

export class ClaimStatusCode extends ModelBase {
  name: string;
  description: string;
}
