using Aafp.Also.Web.ViewModels;
using System.Threading.Tasks;

namespace Aafp.Also.Web.Tasks.Interfaces
{
    public interface IPostCourseTasks
    {
        Task<PostCourseViewModel> GetActivityPostCourse(int activityNumber, string webLogin);

        Task<bool> SavePostCourse(PostCourseSubmissionViewModel model);
    }
}
