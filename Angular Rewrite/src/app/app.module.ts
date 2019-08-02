import 'hammerjs';

// Routing
import { AppRoutingModule } from './app-routing.module';

// angular imports
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

// angular generics
import {
  GenericElementModule, GenericAlertModule, GenericModalModule,
  GenericTableModule, GenericButtonModule, GenericServiceModule,
  GenericDataSourceModule, GenericNavigationModule,
  GenericTablePagerModule, GenericTableFilterModule, GenericInputModule
} from 'angular-generics';

// angular material
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material';
import { MatTabsModule } from '@angular/material/tabs';

// components
import { AppComponent } from './app.component';
import { InventoryComponent } from './components/inventory/inventory.component';
import { NavigationComponent } from './components/nav/nav.component';
import { CreateClaimComponent } from './components/claim/create/create-claim.component';
import { ClaimPreviewComponent } from './components/claim/preview/claim-preview.component';
import { ClaimSummaryComponent } from './components/claim/summary/claim-summary.component';
import { ClaimImportsComponent } from './components/claim/imports/claim-imports.component';
import { ClaimExportsComponent } from './components/claim/exports/claim-exports.component';
import { ClaimManufactureComponent } from './components/claim/manufacture/claim-manufacture.component';
import { ClaimTotalsComponent } from './components/claim/totals/claim-totals.component';
import { ClaimDocumentsComponent } from './components/claim/documents/claim-documents.component';
import { MatchingResultsComponent } from './components/claim/matching-results/matching-results.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    InventoryComponent,
    CreateClaimComponent,
    ClaimPreviewComponent,
    ClaimSummaryComponent,
    ClaimImportsComponent,
    ClaimExportsComponent,
    ClaimManufactureComponent,
    ClaimTotalsComponent,
    ClaimDocumentsComponent,
    MatchingResultsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    MatMenuModule,
    MatToolbarModule,
    MatGridListModule,
    MatIconModule,
    MatSidenavModule,
    MatDividerModule,
    MatListModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatSelectModule,
    MatOptionModule,
    MatTabsModule,
    MatSortModule,

    GenericElementModule,
    GenericAlertModule,
    GenericTableModule,
    GenericButtonModule,
    GenericServiceModule,
    GenericDataSourceModule,
    GenericNavigationModule,
    GenericTablePagerModule,
    GenericTableFilterModule,
    GenericInputModule,
    GenericModalModule
  ],
  providers: [

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
