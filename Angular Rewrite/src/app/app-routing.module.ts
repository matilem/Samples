import { ClaimImportsComponent } from './components/claim/imports/claim-imports.component';
import { ClaimSummaryComponent } from './components/claim/summary/claim-summary.component';
import { ClaimPreviewComponent } from './components/claim/preview/claim-preview.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InventoryComponent } from './components/inventory/inventory.component';
import { CreateClaimComponent } from './components/claim/create/create-claim.component';
import { ClaimExportsComponent } from './components/claim/exports/claim-exports.component';
import { ClaimManufactureComponent } from './components/claim/manufacture/claim-manufacture.component';
import { ClaimTotalsComponent } from './components/claim/totals/claim-totals.component';
import { ClaimDocumentsComponent } from './components/claim/documents/claim-documents.component';

const routes: Routes = [
  // Service
  // { path: 'service', component: ServiceListComponent },
  // { path: 'service/:sid', component: ServiceDetailComponent },
  // { path: 'service/:sid/update/new', component: ServiceUpdateComponent },
  // { path: 'service/:sid/update/:uid', component: ServiceUpdateComponent },

  { path: 'inventory', component: InventoryComponent },
  { path: 'claim/new', component: CreateClaimComponent },

  { path: 'claim-preview', component: ClaimPreviewComponent },
  { path: 'claim-summary', component: ClaimSummaryComponent },
  { path: 'claim-imports', component: ClaimImportsComponent },
  { path: 'claim-export-articles', component: ClaimExportsComponent },
  { path: 'claim-manufacture-articles', component: ClaimManufactureComponent },
  { path: 'claim-totals', component: ClaimTotalsComponent },
  { path: 'claim-documents', component: ClaimDocumentsComponent },


  { path: '', redirectTo: '/inventory', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
