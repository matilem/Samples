using Aafp.Also.Api.Models;
using System;

namespace Aafp.Also.Api.Daos.Commands.Interfaces
{
    public interface IInstructorCommand
    {
        Instructor GetByKey(Guid Key);

        void Store(Instructor instructor);

        void Delete(Instructor instructor);
    }
}
