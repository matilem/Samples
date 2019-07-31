using System.Collections.Generic;
using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos.Lms;

namespace Aafp.Cme.Api.Tasks.Interfaces
{
    public interface ILmsTasks
    {
        Task<int> GetLmsUserId(string webLogin);

        Task<int> GetLmsCourseId(string courseId);

        Task<int> GetLmsCourseEnrollment(int userId, int courseId);

        Task<int> CreateUser(string webLogin);

        Task<int> EnrollUser(LmsEnrollmentPostDto dto);

        Task<List<LmsCartItemDto>> GetUserCart(int lmsUserId);

        Task<bool> DeleteUserCartItem(int cartItemId);

        bool CheckUserAccess(string webLogin, string url);
    }
}
