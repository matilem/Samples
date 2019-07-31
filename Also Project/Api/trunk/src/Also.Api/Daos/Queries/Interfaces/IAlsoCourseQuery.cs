using Aafp.Also.Api.Dtos;
using System;

namespace Aafp.Also.Api.Daos.Queries.Interfaces
{
    public interface IAlsoCourseQuery
    {
        AlsoCourseDto GetAlsoCourse(Guid activityKey);
    }
}