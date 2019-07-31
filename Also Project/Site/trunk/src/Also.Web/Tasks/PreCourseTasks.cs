using Aafp.Also.Web.Tasks.Interfaces;
using Aafp.Also.Web.ViewModels;
using ApiClientHelper.Components;
using System.Net;
using System.Threading.Tasks;

namespace Aafp.Also.Web.Tasks
{
    public class PreCourseTasks : IPreCourseTasks
    {
        public async Task<PreCourseViewModel> GetActivityPrecourse(int activityNumber, string webLogin)
        {
            var result = await HttpClientHelper.GetJson<PreCourseViewModel>(ApplicationConfig.AlsoServiceUrl, $"precourse/{activityNumber}/{webLogin}/");

            return result.StatusCode == HttpStatusCode.OK ? result.Data : new PreCourseViewModel();
        }

        public async Task<IndividualViewModel> VerifyCstId(string cstId)
        {
            var result = await HttpClientHelper.GetJson<IndividualViewModel>(ApplicationConfig.AlsoServiceUrl, $"precourse/verify/{cstId}/");

            return result.StatusCode == HttpStatusCode.OK ? result.Data : new IndividualViewModel();
        }

        public async Task<bool> SavePreCourse(PreCourseSubmissionViewModel model)
        {
            var success = false;
            var result = await HttpClientHelper.PostJson<bool>(ApplicationConfig.AlsoServiceUrl, "precourse/save", model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return success;
        }
    }
}