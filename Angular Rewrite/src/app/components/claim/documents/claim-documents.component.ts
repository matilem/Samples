import { Component, OnInit, ViewChild } from '@angular/core';
import { GenericTableComponent, GenericTableColumn } from 'angular-generics';

class DocumentActions {
  documentName: string;
  actions: string[] = [];
}

@Component({
  selector: 'hamilton-claim-documents',
  templateUrl: './claim-documents.component.html'
})
export class ClaimDocumentsComponent implements OnInit {
  @ViewChild('documentTable', { static: false }) dataTable: GenericTableComponent<DocumentActions>;
  columns: GenericTableColumn[] = [
    new GenericTableColumn({
      id: 'documentName',
      display: 'Document Name',
      dataType: 'string',
      cell: (element: DocumentActions) => `${element.documentName}`,
    }),
    new GenericTableColumn({
      id: 'actions',
      display: 'Action',
      dataType: 'string',
      cell: (element: DocumentActions) => `${element.actions}`,
    }),
  ];

  constructor() { }

  ngOnInit() {
  }

}
