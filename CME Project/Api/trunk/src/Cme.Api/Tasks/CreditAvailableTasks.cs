using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Tasks.Interfaces;

namespace Aafp.Cme.Api.Tasks
{
    public class CreditAvailableTasks : ICreditAvailableTasks
    {
        public IAemTasks AemTasks { get; set; }

        public ICreditAvailableQuery CreditAvailableQuery { get; set; }

        public IIndividualTasks IndividualTasks { get; set; }

        public IAssessmentTasks AssessmentTasks { get; set; }

        public async Task<List<CreditAvailableDto>> GetAllByCustomer(string webLogin)
        {
            var items = new List<CreditAvailableDto>();
            items.AddRange(await GetPurchasedByCustomer(webLogin));
            items.AddRange(await GetSubscriptionsByCustomer(webLogin));
            items.AddRange(await GetFreeItems());
            items = items.OrderByDescending(x => x.TransactionDate).ToList();

            return items.OrderBy(x => x.ExpirationDate).ThenBy(x => x.Title).ToList(); 
        }

        public async Task<List<CreditAvailableDto>> GetPurchasedByCustomer(string webLogin)
        {
            var list = new List<CreditAvailableDto>();
            var customer = await IndividualTasks.GetIndividualByWebLogin(webLogin);
            var items = CreditAvailableQuery.GetPurchasedByCustomer(customer.Key);

            foreach(var item in items)
            {
                item.AssessmentGroupKey = AssessmentTasks.GetAssessmentByActivityNumber(item.ActivityNumber);
                item.IsMember = customer.IsMember;
            }

            list = MergeSameProducts(items);
            list = list.Where(x => x.CreditsAvailable != x.CreditsReported).ToList();

            return list.OrderBy(x => x.ExpirationDate).ThenBy(x => x.Title).ToList(); 
        }

        public async Task<List<CreditAvailableDto>> GetSubscriptionsByCustomer(string webLogin)
        {
            var list = new List<CreditAvailableDto>();
            var customer = await IndividualTasks.GetIndividualByWebLogin(webLogin);
            var items = CreditAvailableQuery.GetSubscriptionsByCustomer(customer.Key);

            // have to remove the AFP result that is returned automatically if the user is not a member
            if (!customer.IsMember)
                items.Remove(items.Find(x => x.ProductKey == new Guid("11111111-1111-1111-1111-111111111111")));
            else
                items.Remove(items.Find(x => x.ProductKey != new Guid("11111111-1111-1111-1111-111111111111") && x.Title == "American Family Physician"));

            list = MergeSameProducts(items);
            list = list.Where(x => x.CreditsAvailable != x.CreditsReported).ToList();

            return list.OrderBy(x => x.ExpirationDate).ThenBy(x => x.Title).ToList();
        }

        public async Task<List<CreditAvailableDto>> GetCompletedByCustomer(string webLogin)
        {
            var list = new List<CreditAvailableDto>();
            var items = new List<CreditAvailableDto>();
            var customer = await IndividualTasks.GetIndividualByWebLogin(webLogin);

            items.AddRange(CreditAvailableQuery.GetPurchasedByCustomer(customer.Key));
            items.AddRange(CreditAvailableQuery.GetSubscriptionsByCustomer(customer.Key));
            items = items.OrderBy(x => x.TransactionDate).ToList();

            // have to remove the AFP result that is returned automatically if the user is not a member
            if (!customer.IsMember)
                items.Remove(items.Find(x => x.ProductKey == new Guid("11111111-1111-1111-1111-111111111111")));

            list = MergeSameProducts(items);
            list = list.Where(x => x.CreditsAvailable == x.CreditsReported).ToList();

            return list;
        }

        public async Task<List<CreditAvailableDto>> GetFreeItems()
        {
            var results = await AemTasks.GetFreeItems();
            var viewModel = AutoMapper.Mapper.Map(results.Results, new List<CreditAvailableDto>());
            
            return viewModel;
        }

        public async Task<CreditAvailableStatsDto> GetCreditTotals(string webLogin)
        {
            var viewModel = new CreditAvailableStatsDto();

            var purchasedItems = await GetPurchasedByCustomer(webLogin);
            purchasedItems = purchasedItems.GroupBy(x => x.ActivityNumber).Select(y => y.First()).ToList();
            var totalPurchasedCredits = purchasedItems.Sum(item => item.CreditsAvailable - item.CreditsReported);
            viewModel.CreditsPurchased = Math.Round(totalPurchasedCredits, MidpointRounding.AwayFromZero);

            var expiringItems = await GetAllByCustomer(webLogin);
            expiringItems = expiringItems.GroupBy(x => x.ActivityNumber).Select(y => y.First()).ToList(); 
            var totalExpiringCredits = expiringItems.Where(x => x.ExpirationDate.HasValue && (x.ExpirationDate.Value - DateTime.Today).TotalDays < 30).Sum(item => item.CreditsAvailable - item.CreditsReported);
            viewModel.CreditsExpiring = Math.Round(totalExpiringCredits, MidpointRounding.AwayFromZero);


            var quizItems = await GetSubscriptionsByCustomer(webLogin);
            var totalQuizCredits = quizItems.Sum(item => item.CreditsAvailable - item.CreditsReported);
            viewModel.QuizzesAvailable = Math.Round(totalQuizCredits, MidpointRounding.AwayFromZero);

            return viewModel;
        }

        private List<CreditAvailableDto> MergeSameProducts(List<CreditAvailableDto> items)
        {
            var list = new List<CreditAvailableDto>();

            foreach (var item in items)
            {
                if (list.All(x => x.ProductKey != item.ProductKey))
                {
                    list.Add(item);
                }
                else
                {
                    var existingItem = list.First(x => x.ProductKey == item.ProductKey);
                    existingItem.CreditsAvailable = existingItem.CreditsAvailable + item.CreditsAvailable;
                    existingItem.CreditsReported = existingItem.CreditsReported + item.CreditsReported;
                }
            }

            return list;
        }
    }
}