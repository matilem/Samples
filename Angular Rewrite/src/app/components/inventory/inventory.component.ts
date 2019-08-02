import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { InventoryImport } from 'src/app/models/inventory-import';
import { InventoryImportTable } from 'src/app/table-definitions/inventory-import';
import { GenericTableColumn, GenericTableComponent, GenericSearchRequest, GenericApiService } from 'angular-generics';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'hamilton-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css'],
})
export class InventoryComponent implements OnInit, AfterViewInit {
  @ViewChild('dataTable', { static: false }) dataTable: GenericTableComponent<InventoryImport>;

  inventoryRequest: GenericSearchRequest = new GenericSearchRequest();
  columns: GenericTableColumn[];

  constructor(public api: GenericApiService) {
    this.columns = new InventoryImportTable().getColumns();
    this.inventoryRequest.endpoint = `${environment.apiRoot}/imports/search`;
    this.inventoryRequest.method = 'search';
  }

  ngOnInit() {

  }

  ngAfterViewInit() {
    this.dataTable.search();
  }

  filterSearch(e: { [field: string]: string; }) {
    this.inventoryRequest.filter = e;
    this.dataTable.search();
  }

  pageChanged(e: { pageNumber: number, takeAmount: number }) {
    this.inventoryRequest.page = e.pageNumber;
    this.inventoryRequest.take = e.takeAmount;

    this.dataTable.search();
  }
}
