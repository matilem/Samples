using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface IAemTasks
    {
        Task<AemCmeDto> GetFreeItems();
    }
}
