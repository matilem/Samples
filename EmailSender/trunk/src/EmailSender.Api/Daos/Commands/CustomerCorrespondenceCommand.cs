using Aafp.EmailSender.Api.Daos.Commands.Interfaces;
using Aafp.EmailSender.Api.Models;

namespace Aafp.EmailSender.Api.Daos.Commands
{
    public class CustomerCorrespondenceCommand : GenericCommand<CustomerCorrespondence>, ICustomerCorrespondenceCommand
    {
        public new void Store(CustomerCorrespondence correspondence)
        {
            base.Store(correspondence);
            Session.Flush();
        }
    }
}