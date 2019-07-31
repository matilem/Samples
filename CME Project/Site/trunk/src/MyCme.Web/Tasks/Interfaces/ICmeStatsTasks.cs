using System.Threading.Tasks;

namespace Aafp.MyCme.Web.Tasks.Interfaces
{
    public interface ICmeStatsTasks
    {
        Task<string> GetCmeStatsHtml(string webLogin);
    }
}
