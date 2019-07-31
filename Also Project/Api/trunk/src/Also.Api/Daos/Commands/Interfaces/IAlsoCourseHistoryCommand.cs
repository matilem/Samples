using Aafp.Also.Api.Models;
using System;

namespace Aafp.Also.Api.Daos.Commands.Interfaces
{
    public interface IAlsoCourseHistoryCommand
    {
        AlsoCourseHistory GetByKey(Guid Key);

        void Store(AlsoCourseHistory alsoCourseHistory);
    }
}
