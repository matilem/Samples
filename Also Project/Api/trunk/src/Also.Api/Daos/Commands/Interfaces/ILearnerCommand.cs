using Aafp.Also.Api.Models;
using System;

namespace Aafp.Also.Api.Daos.Commands.Interfaces
{
    public interface ILearnerCommand
    {
        Learner GetByKey(Guid Key);

        void Store(Learner alsoCourse);
    }
}
