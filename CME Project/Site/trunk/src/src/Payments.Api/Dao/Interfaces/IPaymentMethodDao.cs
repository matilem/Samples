using Aafp.Payments.Api.Models;

namespace Aafp.Payments.Api.Dao.Interfaces
{
    public interface IPaymentMethodDao
    {
        PaymentMethod GetByMethod(string method);
    }
}
