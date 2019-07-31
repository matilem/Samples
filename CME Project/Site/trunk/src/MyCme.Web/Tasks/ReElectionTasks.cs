using System.Net;
using System.Threading.Tasks;
using Aafp.MyCme.Web.Tasks.Interfaces;
using Aafp.MyCme.Web.ViewModels;
using ApiClientHelper.Components;

namespace Aafp.MyCme.Web.Tasks
{
    public class ReElectionTasks : IReElectionTasks
    {
        public async Task<ReElectionStatusViewModel> GetReElectionStatusViewModel(string webLogin)
        {
            var result = await HttpClientHelper.GetJson<ReElectionStatusViewModel>(ApplicationConfig.CmeServiceUrl, $"re-election/status/{webLogin}/");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}