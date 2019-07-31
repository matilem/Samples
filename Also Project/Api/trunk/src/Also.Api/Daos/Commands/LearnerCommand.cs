using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;

namespace Aafp.Also.Api.Daos.Commands
{
    public class LearnerCommand : GenericCommand<Learner>, ILearnerCommand
    {
        public new void Store(Learner learner)
        {
            base.Store(learner);
            Session.Flush();
        }
    }
}