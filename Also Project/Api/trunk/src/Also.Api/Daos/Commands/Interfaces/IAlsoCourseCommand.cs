using Aafp.Also.Api.Models;
using System;

namespace Aafp.Also.Api.Daos.Commands.Interfaces
{
    public interface IAlsoCourseCommand 
    {
        AlsoCourse GetByKey(Guid Key);

        void Store(AlsoCourse alsoCourse);
    }
}
