import { Component, OnInit, Input } from '@angular/core';
import { ClaimPreview } from 'src/app/models/claim-preview';

@Component({
  selector: 'hamilton-claim-summary',
  templateUrl: './claim-summary.component.html'
})
export class ClaimSummaryComponent implements OnInit {

  @Input() claimPreview: ClaimPreview;

  constructor() {
    this.claimPreview = new ClaimPreview();
  }

  ngOnInit() {
  }
}
