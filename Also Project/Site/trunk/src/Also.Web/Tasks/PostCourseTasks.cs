using Aafp.Also.Web.Tasks.Interfaces;
using Aafp.Also.Web.ViewModels;
using ApiClientHelper.Components;
using System.Net;
using System.Threading.Tasks;

namespace Aafp.Also.Web.Tasks
{
    public class PostCourseTasks : IPostCourseTasks
    {
        public async Task<PostCourseViewModel> GetActivityPostCourse(int activityNumber, string webLogin)
        {
            var result = await HttpClientHelper.GetJson<PostCourseViewModel>(ApplicationConfig.AlsoServiceUrl, $"postcourse/{activityNumber}/{webLogin}/");

            return result.StatusCode == HttpStatusCode.OK ? result.Data : new PostCourseViewModel();
        }

        public async Task<bool> SavePostCourse(PostCourseSubmissionViewModel model)
        {
            var success = false;
            var result = await HttpClientHelper.PostJson<bool>(ApplicationConfig.AlsoServiceUrl, "postcourse/save", model);

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