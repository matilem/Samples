using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;

namespace Aafp.Also.Api.Tasks
{
    public class InstructorTasks : IInstructorTasks
    {
        public IInstructorCommand InstructorCommand { get; set; }

        public bool RemoveInstructor(InstructorRemoveDto dto)
        {
            var success = false;

            var existingInstructor = InstructorCommand.GetByKey(dto.InstructorKey);

            InstructorCommand.Delete(existingInstructor);

            return success;
        }

    }
}