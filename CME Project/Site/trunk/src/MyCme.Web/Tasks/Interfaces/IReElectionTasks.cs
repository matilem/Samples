using System.Threading.Tasks;
using Aafp.MyCme.Web.ViewModels;

namespace Aafp.MyCme.Web.Tasks.Interfaces
{
    public interface IReElectionTasks
    {
        Task<ReElectionStatusViewModel> GetReElectionStatusViewModel(string webLogin);
    }
}
