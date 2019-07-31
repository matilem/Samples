using Aafp.Also.Api.Models;
using System;

namespace Aafp.Also.Api.Daos.Commands.Interfaces
{
    public interface IAlsoStatusCommand
    {
        AlsoStatus GetByKey(Guid Key);

        void Store(AlsoStatus alsoStatus);
    }
}
