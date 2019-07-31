using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;

namespace Aafp.Also.Api.Daos.Commands
{
    public class AlsoCourseHistoryCommand : GenericCommand<AlsoCourseHistory>, IAlsoCourseHistoryCommand
    {
        public new void Store(AlsoCourseHistory alsoCourseHistory)
        {
            base.Store(alsoCourseHistory);
            Session.Flush();
        }
    }
}