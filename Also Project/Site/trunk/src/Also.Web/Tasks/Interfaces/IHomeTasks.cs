using Aafp.Also.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aafp.Also.Web.Tasks.Interfaces
{
    public interface IHomeTasks
    {
        Task<List<CourseListViewModel>> GetCourseListByWebLogin(string webLogin);
    }
}
