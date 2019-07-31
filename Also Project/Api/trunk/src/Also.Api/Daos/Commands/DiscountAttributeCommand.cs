using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;

namespace Aafp.Also.Api.Daos.Commands
{
    public class DiscountAttributeCommand : GenericCommand<PriceAttribute>, IDiscountAttributeCommand
    {
        public new void Store(PriceAttribute priceAttribute)
        {
            base.Store(priceAttribute);
            Session.Flush();
        }
    }
}