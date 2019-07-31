using System.Threading.Tasks;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface ICmeStatsTasks
    {
        ICreditAvailableTasks CreditAvailableTasks { get; set; }

        IReElectionTasks ReElectionTasks { get; set; }

        Task<string> GetCmeStatsHtml(string webLogin);

        string GetCmeStatsHtmlForUnauthenticatedUser();
    }
}
