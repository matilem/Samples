using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;

namespace Aafp.Also.Api.Daos.Commands
{
    public class AlsoCourseCommand : GenericCommand<AlsoCourse>, IAlsoCourseCommand
    {
        public new void Store(AlsoCourse alsoCourse)
        {
            base.Store(alsoCourse);
            Session.Flush();
            Session.GetIdentifier(alsoCourse);
        }
    }
}