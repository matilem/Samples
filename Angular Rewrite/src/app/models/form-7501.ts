import { Merchandise } from './merchandise';
import { CountryCode, TransportationCode, PortCode, BondTypeCode, EntryTypeCode } from './codes';
import { Client } from './client';
import { ModelBase } from './model-base';
import { OtherFee } from './other-fee';

export class Form7501 extends ModelBase {
  entryNumber: string;
  summaryDate: Date;
  suretyNumber: string;
  entryDate: Date;
  importingCarrier: string;
  importDate: Date;
  billOfLading: string;
  manufactureId: string;
  exportDate: Date;
  immediateTransportationNumber: string;
  immediateTransportationDate: Date;
  missingDocumentCodes: string;
  foreignPortOfLading: string;

  locationOfGoods: string;
  consigneeNumber: string;
  importerNumber: string;
  referenceNumber: string;
  consigneeNameAddress: string;
  importerNameAddress: string;
  totalEnteredValue: number;
  duty: number;
  tax: number;
  otherFeeTotal: number;
  total: number;
  isImporterAsShownOnForm: boolean;
  isOwnerPurchaserAgent: boolean;
  pricesAreTrue: boolean;
  pricesAreTrueBestBelief: boolean;
  declarantName: string;
  declarantTitle: string;
  declarantSignature: Uint8Array;
  declarantDate: Date;
  brokerFilerName: string;
  brokerFilerAddress: string;
  brokerFilerCity: string;
  brokerFilerState: string;
  brokerFilerZip: string;
  brokerFilerPhone: string;
  internalReferenceNumber: string;

  merchandise: Merchandise[];
  exportingCountry: CountryCode;
  transportationCode: TransportationCode;
  portOfUnlading: PortCode;
  countryOfOrigin: CountryCode;
  bondType: BondTypeCode;
  portOfEntry: PortCode;
  entryType: EntryTypeCode;
  client: Client;
  otherFees: OtherFee[];
}
