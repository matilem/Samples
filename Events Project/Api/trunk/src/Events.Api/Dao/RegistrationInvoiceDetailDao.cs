using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class RegistrationInvoiceDetailDao: GenericDao<RegistrationInvoiceDetail>, IRegistrationInvoiceDetailDao
    {
        public List<RegistrationInvoiceDetail> GetInvoiceDetailsByInvoiceCode(string invoiceCode)
        {
            return Session.Query<RegistrationInvoiceDetail>().Where(x => x.InvoiceCode == invoiceCode).ToList();
        }

        public bool UpdateInvoiceGuestCapacity(Guid invoiceKey, decimal qty)
        {
            var sql = "UPDATE ac_invoice_detail SET ivd_qty = :@Qty WHERE ivd_key = :@IvdKey";
            var query = Session.CreateSQLQuery(sql)
                .SetParameter("@Qty", qty)
                .SetParameter("@IvdKey", invoiceKey);
            var result = query.ExecuteUpdate();

            return result == 1;
        }
    }
}