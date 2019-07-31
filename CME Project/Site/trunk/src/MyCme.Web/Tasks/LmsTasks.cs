using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aafp.MyCme.Web.Helpers;
using Aafp.MyCme.Web.Tasks.Interfaces;
using Aafp.MyCme.Web.ViewModels;

namespace Aafp.MyCme.Web.Tasks
{
    public class LmsTasks : ILmsTasks
    {
        public async Task<LmsAccessViewModel> AccessCourse(string webLogin, string courseId)
        {
            var viewModel = new LmsAccessViewModel();
            
            var hasAccess = await LmsHelper.CheckAccess(webLogin, courseId);

            if (hasAccess.HasValue)
            {
                if (hasAccess.Value)
                {
                    viewModel.HasAccess = true;
                    var lmsUserId = await GetUser(webLogin);

                    if (lmsUserId == null || lmsUserId <= 0)
                    {
                        viewModel.HasLmsCommunicationError = true;
                        return viewModel;
                    }

                    var lmsCourseId = await LmsHelper.GetCourse(courseId);

                    if (lmsCourseId == null || lmsCourseId <= 0)
                    {
                        viewModel.HasLmsCommunicationError = true;
                        return viewModel;
                    }

                    var enrollmentId = await GetEnrollment(lmsUserId.Value, lmsCourseId.Value);

                    if (enrollmentId == null)
                    {
                        viewModel.HasLmsCommunicationError = true;
                        return viewModel;
                    }

                    viewModel.HasAccess = true;
                    viewModel.CourseId = lmsCourseId.Value;

                    return viewModel;
                }

                return viewModel;
            }

            viewModel.HasLmsCommunicationError = true;

            return viewModel;
        }

        public async Task<List<LmsCartItemViewModel>> TransferCart(int lmsUserId)
        {
            var viewModel = new List<LmsCartItemViewModel>();
            var cartItems = await LmsHelper.TransferCart(lmsUserId);

            foreach (var item in cartItems)
            {
                var viewModelItem = new LmsCartItemViewModel
                {
                    ProductCode = item.Cart_Item_Id,
                    Quantity = item.Qty
                };
                viewModel.Add(viewModelItem);
            }

            return viewModel;
        }

        private async Task<int?> GetUser(string webLogin)
        {
            var lmsUserId = await LmsHelper.GetUser(webLogin);

            if (lmsUserId == null)
                return null;

            if (lmsUserId.Value <= 0)
            {
                lmsUserId = await LmsHelper.CreateUser(webLogin);

                return lmsUserId;
            }

            return lmsUserId;
        }

        private async Task<int?> GetEnrollment(int lmsUserId, int lmsCourseid)
        {
            var enrollmentId = await LmsHelper.GetEnrollment(lmsUserId, lmsCourseid);

            if (enrollmentId == null)
                return null;

            if (enrollmentId <= 0)
            {
                enrollmentId = await LmsHelper.CreateEnrollment(lmsUserId, lmsCourseid);

                return enrollmentId;
            }

            return enrollmentId;
        }
    }
}