using Aafp.Also.Web.Tasks.Interfaces;
using Aafp.Also.Web.ViewModels;
using ApiClientHelper.Components;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Aafp.Also.Web.Tasks
{
    public class HomeTasks : IHomeTasks
    {
        public async Task<List<CourseListViewModel>> GetCourseListByWebLogin(string webLogin)
        {
            var result = await HttpClientHelper.GetJson<List<CourseListViewModel>>(ApplicationConfig.AlsoServiceUrl, $"activity/{webLogin}/");

            return result.StatusCode == HttpStatusCode.OK ? result.Data : new List<CourseListViewModel>();
        }
    }
}