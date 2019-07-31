using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;

namespace Aafp.Also.Api.Daos.Commands
{
    public class AlsoStatusCommand : GenericCommand<AlsoStatus>, IAlsoStatusCommand
    {
        public new void Store(AlsoStatus alsoStatus)
        {
            base.Store(alsoStatus);
            Session.Flush();
        }
    }
}