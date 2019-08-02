import { Client } from './client';
import { ClaimStatusCode } from './codes';
import { MatchedPair } from './matched-pair';
import { Provision } from './provision';
import { ModelBase } from './model-base';

export class Claim  extends ModelBase {
  client: Client;
  status: ClaimStatusCode;
  provision: Provision;
  matchedPairs: MatchedPair[];
  internalId: string;
}
