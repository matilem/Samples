using Aafp.EmailSender.Api.Models;

namespace Aafp.EmailSender.Api.Daos.Commands.Interfaces
{
    public interface ICustomerCorrespondenceCommand
    {
        void Store(CustomerCorrespondence customerCorrespondence);
    }
}
