using System;
using System.Net;
using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;
using ApiClientHelper.Components;

namespace Aafp.Cme.Api.Tasks
{
    public class AemTasks : IAemTasks
    {
        private string aemService = ApplicationConfig.AemBaseUrl;

        public async Task<AemCmeDto> GetFreeItems()
        {
            var data = new AemCmeDto();

            try
            {
                var result = await HttpClientHelper.GetJson<AemCmeDto>(aemService, "/content/aafp/cme/browse/free/jcr:content/contentpar/gridblock/listtagcard.feed");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    data = result.Data;
                }
                else
                {
                    throw new ServiceException(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }
    }
}