using System.Threading.Tasks;
using System.Web.Mvc;
using Aafp.MyCme.Web.ViewModels;

namespace Aafp.MyCme.Web.Tasks.Interfaces
{
    public interface ICmeActivityTasks
    {
        Task<CmeActivityViewModel> GetCmeActivies(string activityNumber, string webLogin);
    }
}