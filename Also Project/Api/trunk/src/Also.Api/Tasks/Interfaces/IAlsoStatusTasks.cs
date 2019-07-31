using Aafp.Also.Api.Dtos;
using System;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface IAlsoStatusTasks
    {
        bool SaveAlsoCourseHistory(AlsoCourseHistoryDto dto, string webLogin);

        Task<bool> UpdateInstructorStatus(Guid CustomerKey, string alsoCourseType, string webLogin);
    }
}
