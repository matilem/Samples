using System.Linq;
using Aafp.Payments.Api.Dao.Interfaces;
using Aafp.Payments.Api.Models;
using NHibernate.Linq;

namespace Aafp.Payments.Api.Dao
{
    public class PaymentMethodDao : GenericDao<PaymentMethod>, IPaymentMethodDao
    {
        public PaymentMethod GetByMethod(string method)
        {
            return Session.Query<PaymentMethod>().FirstOrDefault(x => x.Method.ToLower() == method.ToLower());
        }
    }
}