using System;
using Aafp.Events.Admin.ViewModels.Registration;
using ApiClientHelper.Components;

namespace Aafp.Events.Admin.Tasks.Interfaces
{
    public interface IBadgeTasks
    {
        AafpServiceFileResult GetRegistrantBadgePdf(Guid registrantkey);

        AafpServiceFileResult GetRegistrantBadgePdfAll(Guid registrantkey);

        AafpServiceFileResult GetRegistrantSessionBadgePdf(Guid sessionKey);

        AafpServiceFileResult GetRegistrantSessionBadgePdfs(Guid registrantKey);

        PrintEventBadgeViewModel GetPrintEventBadgeViewModel(Guid eventKey);

        AafpServiceFileResult GetEventPdfs(PrintEventBadgeViewModel model);

        AafpServiceFileResult GetInvoice(Guid invoiceKey);
    }
}
