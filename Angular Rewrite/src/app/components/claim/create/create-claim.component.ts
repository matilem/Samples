import { Component, OnInit } from '@angular/core';
import { GenericApiService } from 'angular-generics';
import { environment } from 'src/environments/environment';
import { MatchRequest } from 'src/app/models/match-request';

@Component({
  selector: 'hamilton-claim',
  templateUrl: './create-claim.component.html'
})
export class CreateClaimComponent implements OnInit {

  showClaimRequest: boolean = true;
  showMatchingResults: boolean = false;
  isChecked: boolean = true;
  isMatched: boolean = false;
  hasPreview: boolean = false;
  showPreview: boolean = false;

  match: MatchRequest;

  // API Select Sources
  clientSource: string = `${environment.apiRoot}/clients`;
  provisionSource: string = `${environment.apiRoot}/provisions`;
  formSource: string = `${environment.apiRoot}/forms`;
  batchSource: string = `${environment.apiRoot}/batches`;
  countrySource: string = `${environment.apiRoot}/countries`;
  partSource: string = `${environment.apiRoot}/parts`;
  statusSource: string = `${environment.apiRoot}/statustypes`;

  // Manual Select Options
  matchBy = [
    { id: 'part', name: 'Part' },
    { id: 'hts', name: 'HTS' }
  ];
  exportTypes = [
    { id: 'regular', name: 'Regular' },
    { id: 'unclaimed', name: 'Unclaimed Balances' }
  ];
  importPriority = [
    { id: 'hdf', name: 'Highest Duty First' },
    { id: 'ldf', name: 'Lowest Duty First' },
    { id: 'fifo', name: 'FIFO - Oldest' },
    { id: 'lifo', name: 'LIFO - Newest' }
  ];
  exportPriority = [
    { id: 'hvf', name: 'Highest Value First' },
    { id: 'lvf', name: 'Lowest Value First' },
    { id: 'fifo', name: 'FIFO - Oldest' },
    { id: 'lifo', name: 'LIFO - Newest' }
  ];

  constructor(public api: GenericApiService) {
    this.match = new MatchRequest();
  }

  ngOnInit() {

  }

  // on match click
  getMatches() {
    console.log(this.match);
    this.api
      .post(`${environment.apiRoot}/claims/match`, this.match)
      .then(data => {
        this.isMatched = true;
      });
  }

  // on preview click
  getPreview() {
    this.api
      .get(`${environment.apiRoot}/claims/${this.match.clientId}/preview`)
      .then(data => {
        this.hasPreview = true;
      });
  }

  // on claim click
  createClaim() {
    // this.http
    //   .post<ClaimPreview>(
    //     `${environment.apiRoot}/claim/create`,
    //     this.claimPreview
    //   )
    //   .then(data => {
    //     console.log(data);
    //   });
  }
}
