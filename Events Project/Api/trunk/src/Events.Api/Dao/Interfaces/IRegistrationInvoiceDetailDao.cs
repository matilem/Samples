using System;
using System.Collections.Generic;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IRegistrationInvoiceDetailDao
    {
        List<RegistrationInvoiceDetail> GetInvoiceDetailsByInvoiceCode(string invoiceCode);

        bool UpdateInvoiceGuestCapacity(Guid invoiceKey, decimal qty);
    }
}
