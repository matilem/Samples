using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Tasks.Interfaces;

namespace Aafp.Cme.Api.Tasks
{
    public class InvoiceTasks : IInvoiceTasks
    {
        public IInvoiceQuery InvoiceQuery { get; set; }

        public bool CheckUrlAccess(string webLogin, string url)
        {
            return InvoiceQuery.CheckUrlAccess(webLogin, url);
        }
    }
}