using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;

namespace Aafp.Also.Api.Daos.Commands
{
    public class DiscountCommand : GenericCommand<Price>, IDiscountCommand
    {
        public new void Store(Price price)
        {
            base.Store(price);
            Session.Flush();
        }
    }
}