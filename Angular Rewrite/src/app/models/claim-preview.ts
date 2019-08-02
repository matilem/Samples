import { ClaimTotal } from './claim-total';
import { ClaimExportArticle} from './claim-export-articles';
import { ClaimSummary } from './claim-summary';
import { ModelBase } from './model-base';
import { ClaimDocument } from './claim-document';
import { ClaimImport } from './claim-import';
import { ClaimManufactureArticle } from './claim-manufacture-article';

export class ClaimPreview extends ModelBase {
    claimSummary: ClaimSummary;
    claimImports: ClaimImport[];
    claimExportArticles: ClaimExportArticle[];
    claimManufactureArticles: ClaimManufactureArticle[];
    claimTotals: ClaimTotal;
    claimDocuments: ClaimDocument[];
}
