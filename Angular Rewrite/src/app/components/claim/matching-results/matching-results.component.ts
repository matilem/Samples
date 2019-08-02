import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { GenericTableColumn, GenericTableComponent, GenericSearchRequest, GenericApiService } from 'angular-generics';
import { environment } from 'src/environments/environment';
import { MatchingResult } from 'src/app/models/matching-result';
import { MatchingResultTable } from 'src/app/table-definitions/matching-results';

@Component({
  selector: 'hamilton-matching-results',
  templateUrl: './matching-results.component.html',
  styleUrls: ['./matching-results.component.css']
})
export class MatchingResultsComponent implements OnInit {
  @ViewChild('dataTable', { static: false }) dataTable: GenericTableComponent<MatchingResult>;

  matchingResultsRequest: GenericSearchRequest = new GenericSearchRequest();
  columns: GenericTableColumn[];

  constructor(public api: GenericApiService) {
    this.columns = new MatchingResultTable().getColumns();
    this.matchingResultsRequest.endpoint = `${environment.apiRoot}/claim/1/matching-results`;
  }

  ngOnInit() {

  }
}
