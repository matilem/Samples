using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Aafp.MyCme.Web.Dtos;
using Aafp.MyCme.Web.ViewModels;
using ApiClientHelper.Components;

namespace Aafp.MyCme.Web.Helpers
{
    public class LmsHelper
    {
        public static async Task<bool?> CheckAccess(string webLogin, string courseId)
        {
            var url = $"{ApplicationConfig.BaseUrl}/my-cme/lms/access/{courseId}";

            var result = await HttpClientHelper.GetJson<bool>(ApplicationConfig.CmeServiceUrl, $"lms/user/{webLogin}/check-access?url={url}");

            return result.StatusCode != HttpStatusCode.OK ? new bool?() : result.Data;
        }

        public static async Task<int?> GetUser(string webLogin)
        {
            var result = await HttpClientHelper.GetJson<int>(ApplicationConfig.CmeServiceUrl, $"lms/user/{webLogin}/");

            if (result.StatusCode == HttpStatusCode.NotFound)
                return 0;

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Data;
            
            return new int?();
        }

        public static async Task<int?> CreateUser(string webLogin)
        {
            var result = await HttpClientHelper.PostJson<int>(ApplicationConfig.CmeServiceUrl, $"lms/user/{webLogin}/create", null);

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Data;

            return new int?();
        }

        public static async Task<int?> GetCourse(string courseId)
        {
            var result = await HttpClientHelper.GetJson<int>(ApplicationConfig.CmeServiceUrl, $"lms/course/{courseId}");

            if (result.StatusCode == HttpStatusCode.NotFound)
                return 0;

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Data;

            return new int?();
        }

        public static async Task<int?> GetEnrollment(int lmsUserId, int lmsCourseId)
        {
            var result = await HttpClientHelper.GetJson<int>(ApplicationConfig.CmeServiceUrl, $"enrollment/{lmsUserId}/{lmsCourseId}");

            if (result.StatusCode == HttpStatusCode.NotFound)
                return 0;

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Data;

            return new int?();
        }

        public static async Task<int?> CreateEnrollment(int lmsUserId, int lmsCourseId)
        {
            var dto = new LmsEnrollmentPostDto
            {
                LmsUserId = lmsUserId,
                LmsCourseId = lmsCourseId
            };

            var result = await HttpClientHelper.PostJson<int>(ApplicationConfig.CmeServiceUrl, "enroll", dto);

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Data;

            return new int?();
        }

        public static async Task<List<LmsCartItemDto>> TransferCart(int lmsUserId)
        {
            var result = await HttpClientHelper.GetJson<List<LmsCartItemDto>>(ApplicationConfig.CmeServiceUrl, $"cart/{lmsUserId}");

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Data;

            return null;
        }
    }
}