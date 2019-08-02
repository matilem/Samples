import { Component, OnInit } from '@angular/core';
import { GenericSearchRequest, GenericApiService } from 'angular-generics';
import { environment } from 'src/environments/environment';
import { ClaimPreview } from 'src/app/models/claim-preview';

@Component({
  selector: 'hamilton-claim-preview',
  templateUrl: './claim-preview.component.html',
  styleUrls: ['./claim-preview.component.css']
})
export class ClaimPreviewComponent implements OnInit {
  claimRequest: GenericSearchRequest = new GenericSearchRequest();
  // claimPreview: ClaimPreview;

  constructor(public http: GenericApiService) {
    // this.claimPreview = new ClaimPreview();
  }

  ngOnInit() { }

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
