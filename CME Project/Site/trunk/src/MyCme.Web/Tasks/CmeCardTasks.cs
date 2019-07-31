using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aafp.MyCme.Web.Tasks.Interfaces;
using Aafp.MyCme.Web.ViewModels;
using ApiClientHelper.Components;

namespace Aafp.MyCme.Web.Tasks
{
    public class CmeCardTasks : ICmeCardTasks
    {
        public async Task<CmeCardTotalsViewModel> GetCardTotals(string webLogin)
        {
            var viewModel = new CmeCardTotalsViewModel();

            var purchasedItems = await GetPurchasedItems(webLogin);
            decimal totalPurchasedCredits = purchasedItems.Sum(item => item.CreditsAvailable - item.CreditsReported);
            viewModel.CreditsPurchased = Math.Round(totalPurchasedCredits, MidpointRounding.AwayFromZero);

            var expiringItems = await GetAllItems(webLogin);
            var totalExpiringCredits = expiringItems.Where(x => x.ShowExpirationTag).Sum(item => item.CreditsAvailable - item.CreditsReported);
            viewModel.CreditsExpiring = Math.Round(totalExpiringCredits, MidpointRounding.AwayFromZero);


            var quizItems = await GetSubscriptionItems(webLogin);
            var totalQuizCredits = quizItems.Sum(item => item.CreditsAvailable - item.CreditsReported);
            viewModel.QuizzesAvailable = Math.Round(totalQuizCredits, MidpointRounding.AwayFromZero);

            return viewModel;
        }

        public async Task<List<CmeCardViewModel>> GetAllItems(string webLogin)
        {
            var viewModel = new List<CmeCardViewModel>();
            viewModel.AddRange(await GetPurchasedItems(webLogin));
            viewModel.AddRange(await GetSubscriptionItems(webLogin));
            viewModel.AddRange(await GetFreeItems());

            return viewModel;
        }

        public async Task<List<CmeCardViewModel>> GetPurchasedItems(string webLogin)
        {
            var viewModel = await GetCardData($"available-credits/purchased/{webLogin}/");

            return viewModel;
        }

        public async Task<List<CmeCardViewModel>> GetSubscriptionItems(string webLogin)
        {
            var viewModel = await GetCardData($"available-credits/subscriptions/{webLogin}/");

            return viewModel;
        }

        public async Task<List<CmeCardViewModel>> GetFreeItems()
        {
            var viewModel = await GetCardData("available-credits/free");

            foreach (var item in viewModel)
            {
                item.IsAemCard = true;
            }

            return viewModel;
        }
        
        public async Task<List<CmeCardViewModel>> GetCompletedItems(string webLogin)
        {
            var viewModel = await GetCardData($"available-credits/completed/{webLogin}/");

            foreach (var item in viewModel)
            {
                item.IsCompletedCard = true;
            }

            return viewModel;
        }

        private async Task<List<CmeCardViewModel>> GetCardData(string path)
        {
            var result = await HttpClientHelper.GetJson<List<CmeCardViewModel>>(ApplicationConfig.CmeServiceUrl, path);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}