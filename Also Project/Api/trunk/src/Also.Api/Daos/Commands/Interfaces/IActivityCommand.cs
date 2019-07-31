using Aafp.Also.Api.Models;
using System;

namespace Aafp.Also.Api.Daos.Commands.Interfaces
{
    public interface IActivityCommand
    {
        Activity GetByKey(Guid Key);

        void Store(Activity activity);
    }
}
