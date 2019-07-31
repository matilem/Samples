using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aafp.MyCme.Web.Filters;
using Aafp.MyCme.Web.Tasks.Interfaces;

namespace Aafp.MyCme.Web.Controllers
{
    [RoutePrefix("stats")]
    [SiteMinderAuthenticationFilter(Roles = new[] { "customer" })]
    public class CmeStatsController : Controller
    {
        public ICmeStatsTasks CmeStatsTasks { get; set; }

        [Route("")]
        public async Task<HtmlString> GetCmeStatsHtml()
        {
            var html = await CmeStatsTasks.GetCmeStatsHtml(User.Identity.Name);
            return new HtmlString(html);
        }
    }
}