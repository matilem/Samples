using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;

namespace Aafp.Also.Api.Daos.Commands
{
    public class ActivityCommand : GenericCommand<Activity>, IActivityCommand
    {
        public new void Store(Activity activity)
        {
            base.Store(activity);
            Session.Flush();
        }
    }
}