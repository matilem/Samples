using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aafp.MyCme.Web.Tasks.Interfaces;
using Aafp.MyCme.Web.ViewModels;

namespace Aafp.MyCme.Web.Tasks
{
    public class HomeTasks : IHomeTasks
    {
        public ICmeCardTasks CmeCardTasks { get; set; }

        public async Task<HomeViewModel> GetHomeViewModel(string webLogin)
        {
            var viewModel = new HomeViewModel();
            viewModel.Cards = new List<CmeCardViewModel>();

            try
            {
                viewModel.Cards = await CmeCardTasks.GetPurchasedItems(webLogin); // default to purchased items for initial page view
            }
            catch (Exception)
            {
                viewModel.HasError = true;
                viewModel.ErrorMessage = "We are unable to retrieve results from the server at this time.";
            }

            return viewModel;
        }
    }
}