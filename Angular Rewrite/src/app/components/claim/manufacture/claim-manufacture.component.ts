import { Component, OnInit, ViewChild } from '@angular/core';
import { GenericSearchRequest, GenericTableComponent, GenericTableColumn } from 'angular-generics';
import { ClaimManufactureArticle } from 'src/app/models/claim-manufacture-article';
import { ClaimManufactureTable } from 'src/app/table-definitions/claim-manufacture';

@Component({
  selector: 'hamilton-claim-manufacture',
  templateUrl: './claim-manufacture.component.html'
})
export class ClaimManufactureComponent implements OnInit {
  @ViewChild('claimManufactureTable', { static: false }) dataTable: GenericTableComponent<ClaimManufactureArticle>;

  exportRequest: GenericSearchRequest = new GenericSearchRequest();
  columns: GenericTableColumn[] = [];

  constructor() {
    // this.importRequest.endpoint = `${environment.apiRoot}/imports/search`;
    // this.importRequest.method = 'search';
  }

  ngOnInit() {
    this.columns = new ClaimManufactureTable().getColumns();
  }

}
