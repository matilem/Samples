import { Component, OnInit, ViewChild } from '@angular/core';
import { GenericTableComponent, GenericSearchRequest, GenericTableColumn } from 'angular-generics';
import { ClaimImport } from '../../../models/claim-import';
import { ClaimExportTable } from 'src/app/table-definitions/claim-export';
import { ClaimExportArticle } from 'src/app/models/claim-export-articles';

@Component({
  selector: 'hamilton-claim-exports',
  templateUrl: './claim-exports.component.html'
})
export class ClaimExportsComponent implements OnInit {
  @ViewChild('claimExportTable', { static: false }) dataTable: GenericTableComponent<ClaimExportArticle>;

  exportRequest: GenericSearchRequest = new GenericSearchRequest();
  columns: GenericTableColumn[] = [];

  constructor() {
    // this.importRequest.endpoint = `${environment.apiRoot}/imports/search`;
    // this.importRequest.method = 'search';
  }

  ngOnInit() {
    this.columns = new ClaimExportTable().getColumns();
  }

}
