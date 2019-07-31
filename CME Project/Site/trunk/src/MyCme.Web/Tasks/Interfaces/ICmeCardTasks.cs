using System.Collections.Generic;
using System.Threading.Tasks;
using Aafp.MyCme.Web.ViewModels;

namespace Aafp.MyCme.Web.Tasks.Interfaces
{
    public interface ICmeCardTasks
    {
        Task<CmeCardTotalsViewModel> GetCardTotals(string webLogin);

        Task<List<CmeCardViewModel>> GetAllItems(string webLogin);

        Task<List<CmeCardViewModel>> GetPurchasedItems(string webLogin);

        Task<List<CmeCardViewModel>> GetSubscriptionItems(string webLogin);

        Task<List<CmeCardViewModel>> GetFreeItems();

        Task<List<CmeCardViewModel>> GetCompletedItems(string webLogin);
    }
}
