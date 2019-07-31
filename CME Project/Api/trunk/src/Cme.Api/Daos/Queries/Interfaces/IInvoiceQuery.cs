namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public interface IInvoiceQuery
    {
        bool CheckUrlAccess(string webLogin, string url);
    }
}
