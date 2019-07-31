using Aafp.Also.Api.Dtos;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface IInstructorTasks
    {
        bool RemoveInstructor(InstructorRemoveDto dto);
    }
}
