using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;

namespace Aafp.Also.Api.Daos.Commands
{
    public class InstructorCommand : GenericCommand<Instructor>, IInstructorCommand
    {
        public new void Store(Instructor instructor)
        {
            base.Store(instructor);
            Session.Flush();
        }

        public new void Delete(Instructor instructor)
        {
            base.Delete(instructor);
            Session.Flush();
        }
    }
}