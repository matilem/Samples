using System.Net;
using System.Threading.Tasks;
using Aafp.MyCme.Web.Tasks.Interfaces;
using ApiClientHelper.Components;

namespace Aafp.MyCme.Web.Tasks
{
    public class CmeStatsTasks : ICmeStatsTasks
    {
        public async Task<string> GetCmeStatsHtml(string webLogin)
        {
            var result = await HttpClientHelper.GetJson<string>(ApplicationConfig.CmeServiceUrl, $"cme/stats/{webLogin}/");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}