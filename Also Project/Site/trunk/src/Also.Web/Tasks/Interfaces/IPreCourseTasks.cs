using Aafp.Also.Web.ViewModels;
using System.Threading.Tasks;

namespace Aafp.Also.Web.Tasks.Interfaces
{
    public interface IPreCourseTasks
    {
        Task<PreCourseViewModel> GetActivityPrecourse(int activityNumber, string webLogin);

        Task<IndividualViewModel> VerifyCstId(string cstId);

        Task<bool> SavePreCourse(PreCourseSubmissionViewModel model);
    }
}
