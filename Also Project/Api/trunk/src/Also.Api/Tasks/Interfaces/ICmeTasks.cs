using Aafp.Also.Api.Dtos;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface ICmeTasks
    {
        Task<bool> ReportCmeForAlso(AlsoCreditDto dto);

        Task<bool> ReportTeachingCredits(TeachingCreditDto dto);
    }
}
