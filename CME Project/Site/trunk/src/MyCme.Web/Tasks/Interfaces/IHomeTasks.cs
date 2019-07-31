using System.Threading.Tasks;
using Aafp.MyCme.Web.ViewModels;

namespace Aafp.MyCme.Web.Tasks.Interfaces
{
    public interface IHomeTasks
    {
        Task<HomeViewModel> GetHomeViewModel(string webLogin);
    }
}
