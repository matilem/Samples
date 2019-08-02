import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { GenericTableComponent, GenericTableColumn, GenericSearchRequest } from 'angular-generics';
import { ClaimImport } from 'src/app/models/claim-import';
import { ClaimImportTable } from 'src/app/table-definitions/claim-imports';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'hamilton-claim-imports',
  templateUrl: './claim-imports.component.html'
})
export class ClaimImportsComponent implements OnInit {
  @ViewChild('claimImportTable', { static: false }) dataTable: GenericTableComponent<ClaimImport>;

  importRequest: GenericSearchRequest = new GenericSearchRequest();
  columns: GenericTableColumn[] = [];

  constructor() {
    // this.importRequest.endpoint = `${environment.apiRoot}/imports/search`;
    // this.importRequest.method = 'search';
  }

  ngOnInit() {
    this.columns = new ClaimImportTable().getColumns();
  }
}
