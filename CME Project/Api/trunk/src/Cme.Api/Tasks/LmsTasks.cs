using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aafp.Cme.Api.Dtos.Lms;
using Aafp.Cme.Api.Tasks.Interfaces;

namespace Aafp.Cme.Api.Tasks
{
    public class LmsTasks : ILmsTasks
    {
        private string _lmsService = ApplicationConfig.LmsBaseUrl;

        public ICmeSessionTasks CmeSessionTasks { get; set; }

        public IInvoiceTasks InvoiceTasks { get; set; }

        public IIndividualTasks IndividualTasks { get; set; }

        public bool CheckUserAccess(string webLogin, string url)
        {
            return InvoiceTasks.CheckUrlAccess(webLogin, url);
        }

        public async Task<int> GetLmsUserId(string webLogin)
        {
            var lmsUserId = 0;
            var individual = await IndividualTasks.GetIndividualByWebLogin(webLogin);

            var result = await GetJson<LmsDtoWrapper<LmsUserDto>>(_lmsService, $"authmap.json?authname={individual.CustomerId}");

            if (result != null && result.List.Any())
                lmsUserId = Int32.Parse(result.List[0].Uid);

            return lmsUserId;
        }

        public async Task<int> GetLmsCourseId(string courseId)
        {
            var lmsCourseId = 0;

            var result = await GetJson<LmsDtoWrapper<LmsCourseDto>>(_lmsService, $"course.json?external_id={courseId}");

            if (result != null && result.List.Any())
                lmsCourseId = Int32.Parse(result.List[0].Nid.Id);

            return lmsCourseId;
        }

        public async Task<int> GetLmsCourseEnrollment(int userId, int courseId)
        {
            var enrollmentId = 0;

            var result = await GetJson<LmsDtoWrapper<LmsEnrollmentDto>>(_lmsService, $"course_enrollment.json?uid={userId}&nid={courseId}");

            if (result != null && result.List.Any())
                enrollmentId = Int32.Parse(result.List[0].Eid);

            return enrollmentId;
        }

        public async Task<int> CreateUser(string webLogin)
        {
            var individual = await IndividualTasks.GetIndividualByWebLogin(webLogin);

            var dto = new PostLmsUserDto
            {
                AuthName = individual.CustomerId
            };

            var lmsUserId = 0;

            var result = await PostJson<LmsCreatedUserDto>(_lmsService, "authmap", dto);

            if (result != null && result.Id > 0)
                lmsUserId = result.Id;

            return lmsUserId;
        }

        public async Task<int> EnrollUser(LmsEnrollmentPostDto postDto)
        {
            var enrollmentId = 0;
            var dto = new LmsEnrollmentCreateDto
            {
                Uid = postDto.LmsUserId,
                Nid = postDto.LmsCourseId
            };

            var result = await PostJson<LmsCreatedEnrollmentDto>(_lmsService, "course_enrollment", dto);

            if (result != null && result.Id > 0)
                enrollmentId = result.Id;

            return enrollmentId;
        }

        public async Task<List<LmsCartItemDto>> GetUserCart(int lmsUserId)
        {
            var cartItems = new List<LmsCartItemDto>();

            var result = await GetJson<LmsDtoWrapper<LmsCartItemDto>>(_lmsService, $"uc_cart_item.json?cart_id={lmsUserId}");

            if (result != null && result.List.Any())
                cartItems = result.List;

            return cartItems;
        }

        public async Task<bool> DeleteUserCartItem(int cartItemId)
        {
            var deleted = await GetJson<bool>(_lmsService, $"uc_cart_item/{cartItemId}");

            return deleted;
        }

        private async Task<T> GetJson<T>(string baseUrl, string endPoint)
        {
            T result;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetAuthValue());
                var response = await client.GetAsync(endPoint);

                if (!response.IsSuccessStatusCode)
                {
                    result = default(T);
                }
                else
                {
                    result = await response.Content.ReadAsAsync<T>();
                }
            }

            return result;
        }

        private async Task<T> PostJson<T>(string baseUrl, string endPoint, object data)
        {
            T result;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetAuthValue());
                var response = await client.PostAsJsonAsync(endPoint, data);

                if (!response.IsSuccessStatusCode)
                {
                    result = default(T);
                }
                else
                {
                    result = await response.Content.ReadAsAsync<T>();
                }
            }

            return result;
        }

        private async Task<bool> Delete(string baseUrl, string endPoint)
        {
            var deleted = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetAuthValue());
                var response = await client.DeleteAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    deleted = true;
                }
            }

            return deleted;
        }

        private string GetAuthValue()
        {
            var byteArray = Encoding.ASCII.GetBytes("restws_serviceaccount01:M4TiuUudfzPa");

            return Convert.ToBase64String(byteArray);
        }
    }
}