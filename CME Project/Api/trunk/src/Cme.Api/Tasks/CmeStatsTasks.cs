using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Helpers;
using Aafp.Cme.Api.Tasks.Interfaces;
using Aafp.Cme.Api.Templates;

namespace Aafp.Cme.Api.Tasks
{
    public class CmeStatsTasks : ICmeStatsTasks
    {
        public ICreditAvailableTasks CreditAvailableTasks { get; set; }

        public IReElectionTasks ReElectionTasks { get; set; }

        public async Task<string> GetCmeStatsHtml(string webLogin)
        {
            var model = new CmeStatsDto();
            model.ReElectionInfo = await ReElectionTasks.GetReElectionByWebLogin(webLogin);
            model.CreditAvailableInfo = await CreditAvailableTasks.GetCreditTotals(webLogin);

            var helper = new TemplateHelper<CmeStatsTemplate>();

            return helper.GenerateHtml(new CmeStatsTemplate { Model = model });
        }

        public string GetCmeStatsHtmlForUnauthenticatedUser()
        {
            var model = new CmeStatsDto();
            var helper = new TemplateHelper<CmeStatsUnauthenticatedTemplate>();

            return helper.GenerateHtml(new CmeStatsUnauthenticatedTemplate { Model = model });
        }
    }
}