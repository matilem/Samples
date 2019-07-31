using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using System.Net;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks
{
    public class CmeTasks : ICmeTasks
    {
        private string cmeService = $"{ApplicationConfig.BaseUrl}/cme-api/";
        //private string cmeService = $"http://dev.ams.aafp.org/cme-api/";

        public async Task<bool> ReportCmeForAlso(AlsoCreditDto dto)
        {
            var success = false;
            var result = await HttpClientHelper.PostJson<bool>(cmeService, $"credit/reportcredits/by-session", dto);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        public async Task<bool> ReportTeachingCredits(TeachingCreditDto dto)
        {
            var success = false;
            var result = await HttpClientHelper.PostJson<bool>(cmeService, $"credit/reportcredits/teaching", dto);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }
    }
}