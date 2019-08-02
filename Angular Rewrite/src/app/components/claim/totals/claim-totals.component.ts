import { Component, OnInit, ViewChild } from '@angular/core';
import { ClaimTotal } from 'src/app/models/claim-total';
import { GenericTableComponent, GenericTableColumn } from 'angular-generics';

class AccountingLineItems {
  classCode: string;
  amount: number;
}

@Component({
  selector: 'hamilton-claim-totals',
  templateUrl: './claim-totals.component.html'
})
export class ClaimTotalsComponent implements OnInit {
  @ViewChild('accountingTable', { static: false }) dataTable: GenericTableComponent<AccountingLineItems>;
  columns: GenericTableColumn[] = [
    new GenericTableColumn({
      id: 'classCode',
      display: 'Class Code',
      dataType: 'string',
      cell: (element: AccountingLineItems) => `${element.classCode}`,
    }),
    new GenericTableColumn({
      id: 'amount',
      display: 'Amount',
      dataType: 'number',
      cell: (element: AccountingLineItems) => `${element.amount}`,
    }),
  ];

  claimTotal: ClaimTotal;
  constructor() {
    this.claimTotal = new ClaimTotal();
  }

  ngOnInit() {
  }

}
