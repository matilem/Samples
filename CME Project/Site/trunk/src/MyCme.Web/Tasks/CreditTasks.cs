using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aafp.MyCme.Web.Dtos;
using Aafp.MyCme.Web.Tasks.Interfaces;
using Aafp.MyCme.Web.ViewModels;
using ApiClientHelper.Components;

namespace Aafp.MyCme.Web.Tasks
{
    public class CreditTasks : ICreditTasks
    {
        public async Task<JsonResultViewModel<List<CreditDto>>> SubmitCredits(string[] sessionKeys, string webLogin)
        {
            var viewModel = new JsonResultViewModel<List<CreditDto>>();
            var credits = sessionKeys.Select(key => new Guid(key)).ToArray();

            try
            {
                var result = await HttpClientHelper.PostJson<List<CreditDto>>(ApplicationConfig.CmeServiceUrl, $"credit/reportcredits/{webLogin}/", credits);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new ServiceException(result.ErrorMessage);
                }

                viewModel.Data = result.Data;

                var index = viewModel.Data.FindIndex(x => x.HasError);
                if (index >= 0)
                {
                    foreach (var dto in viewModel.Data)
                    {
                        if (!dto.HasError) continue;
                        var error = "Session Key: " + dto.SessionKey + " Error Message: " + dto.ErrorMessage;

                        viewModel.HasError = true;
                        viewModel.ErrorMessage = error;
                        viewModel.SessionKey = dto.SessionKey;

                        return viewModel;
                    }
                }
            }
            catch (ServiceException ex)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to save your selected credits at this time.";
            }

            return viewModel;
        }
    }
}