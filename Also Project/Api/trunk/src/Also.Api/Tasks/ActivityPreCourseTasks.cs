using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using System.Threading.Tasks;
using System;
using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Models;
using Aafp.Also.Api.Helpers;

namespace Aafp.Also.Api.Tasks
{
    public class ActivityPreCourseTasks : IActivityPreCourseTasks
    {
        public IActivityPreCourseQuery ActivityPreCourseQuery { get; set; }

        public IAlsoCourseQuery AlsoCourseQuery { get; set; }

        public IActivityCommand ActivityCommand { get; set; }

        public IAlsoCourseCommand AlsoCourseCommand { get; set; }

        public IIndividualTasks IndividualTasks { get; set; }

        public IDiscountTasks DiscountTasks { get; set; }

        public IEmailTasks EmailTasks { get; set; }

        public async Task<ActivityPreCourseDto> GetPreCourse(string activityNumber, string webLogin)
        {
            var dto = new ActivityPreCourseDto();
            dto.AlsoCourse = new AlsoCourseDto();

            dto = ActivityPreCourseQuery.GetPreCourse(activityNumber);

            if (dto != null)
            {
                dto.Customer = await IndividualTasks.GetIndividualByWebLogin(webLogin);             
                dto.AlsoCourse = AlsoCourseQuery.GetAlsoCourse(dto.Activity.ActivityKey);
            }

            return dto;
        }

        public async Task<bool> SavePreCourse(ActivityPreCourseSubmissionDto dto)
        {
            var success = false;
            var activity = new Activity();
            
            activity = ActivityCommand.GetByKey(dto.ActivityKey);

            activity.Key = dto.ActivityKey;
            activity.ChangeDate = DateTime.Now;
            activity.ChangeUser = dto.WebLogin;
            activity.DirectorKey = dto.CourseDirectorKey;
            activity.DirectorId = dto.CourseDirectorId;
            activity.DirectorName = dto.CourseDirectorName;
            activity.DirectorEmail = dto.CourseDirectorEmail;
            activity.DirectorPhone = dto.CourseDirectorPhone;
            activity.CoordinatorKey = dto.CourseCoordinatorKey;
            activity.CoordinatorId = dto.CourseCoordinatorId;
            activity.CoordinatorName = dto.CourseCoordinatorName;
            activity.CoordinatorEmail = dto.CourseCoordinatorEmail;
            activity.CoordinatorPhone = dto.CourseCoordinatorPhone;

            dto.ActivityBeginDate = activity.BeginDate;
            dto.ActivityEndDate = activity.EndDate;

            ActivityCommand.Store(activity);

            if (dto.AlsoCourseKey != Guid.Empty)
            {
                success = await UpdateAlsoCourse(dto, activity.EndDate);
            }
            else
            {
                success = await AddAlsoCourse(dto, activity.BeginDate, activity.EndDate);
            }

            return success;
        }

        public async Task<bool> AddAlsoCourse(ActivityPreCourseSubmissionDto dto, DateTime activityStartDate, DateTime activityEndDate)
        {
            var success = false;
            try
            {
                var alsoCourse = new AlsoCourse
                {
                    ActivityKey = dto.ActivityKey,
                    MilitaryKey = dto.MilitaryBranchKey,
                    AddDate = DateTime.Now,
                    AddUser = dto.WebLogin,
                    PriceKey = new Guid()                    
                };

                if (dto.Status == "Submit")
                {
                    alsoCourse.PreCourseSubmittedFlag = true;
                }

                if (dto.Status == "Approve")
                {
                    alsoCourse.PreCourseSubmittedFlag = true;
                    alsoCourse.PreCourseApprovedFlag = true;
                    alsoCourse.PriceKey = CreateDiscount(dto.ActivityNumber, activityEndDate, dto.WebLogin);

                    if (alsoCourse.PriceKey != Guid.Empty)
                    {
                        var alsoEmail = new AlsoMessageDto
                        {
                            DiscountCode = dto.ActivityNumber,
                            ActivityBeginDate = activityStartDate,
                            ActivityEndDate = activityEndDate,
                            ActivityLocation = dto.ActivityLocation,
                            ActivitySponsorName = dto.ActivitySponsorName,
                            CourseDirectorEmail = dto.CourseDirectorEmail,
                            CourseCoordinatorEmail = dto.CourseCoordinatorEmail
                        };

                        try
                        {
                            var email = await SendWelcomeEmail(alsoEmail);
                        }
                        catch (Exception ex)
                        {
                            var message = $"Unable to send ALSO/BLSO welcome email. Activity: {dto.ActivityNumber}, Error: {ex.Message}.";
                            Logger.LogError(message);
                        }
                    }
                }

                if (dto.ActivityCourseType == "ALSO Provider")
                {
                    alsoCourse.PriceKey = ApplicationConfig.ALSOProviderPriceKey;
                }

                if (dto.ActivityCourseType == "ALSO Instructor")
                {
                    alsoCourse.PriceKey = ApplicationConfig.ALSOInstructorPriceKey;
                }
                if (dto.ActivityCourseType == "BLSO Provider")
                {
                    alsoCourse.PriceKey = ApplicationConfig.BLSOProviderPriceKey;
                }
                else
                {
                    alsoCourse.PriceKey = ApplicationConfig.ALSOInstructorPriceKey;
                }

                AlsoCourseCommand.Store(alsoCourse);
                success = true;
            }

            catch (Exception ex)
            {
                var message = $"Unable to add the also course. Activity: {dto.ActivityKey}, User: {dto.WebLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }
            return success;
        }

        public async Task<bool> UpdateAlsoCourse(ActivityPreCourseSubmissionDto dto, DateTime activityEndDate)
        {
            var success = false;
            try
            {
                var alsoCourse = AlsoCourseCommand.GetByKey(dto.AlsoCourseKey);

                alsoCourse.ActivityKey = dto.ActivityKey;
                alsoCourse.MilitaryKey = dto.MilitaryBranchKey;

                if (dto.Status == "Submit")
                {
                    alsoCourse.PreCourseSubmittedFlag = true;
                }

                if (dto.Status == "Approve")
                {
                    alsoCourse.PreCourseSubmittedFlag = true;
                    alsoCourse.PreCourseApprovedFlag = true;
                    alsoCourse.PriceKey = CreateDiscount(dto.ActivityNumber, activityEndDate, dto.WebLogin);

                    if (alsoCourse.PriceKey != Guid.Empty)
                    {
                        var alsoEmail = new AlsoMessageDto
                        {
                            DiscountCode = dto.ActivityNumber,
                            ActivityBeginDate = dto.ActivityBeginDate,
                            ActivityEndDate = dto.ActivityEndDate,
                            ActivityLocation = dto.ActivityLocation,
                            ActivitySponsorName = dto.ActivitySponsorName,
                            CourseDirectorEmail = dto.CourseDirectorEmail,
                            CourseCoordinatorEmail = dto.CourseCoordinatorEmail,
                            ActivityCourseType = dto.ActivityCourseType
                        };

                        try
                        {
                            var email = await SendWelcomeEmail(alsoEmail);
                        }
                        catch (Exception ex)
                        {
                            var message = $"Unable to send ALSO/BLSO welcome email. Activity: {dto.ActivityNumber}, Error: {ex.Message}.";
                            Logger.LogError(message);
                        }
                    }
                }

                alsoCourse.ChangeDate = DateTime.Now;
                alsoCourse.ChangeUser = dto.WebLogin;

                AlsoCourseCommand.Store(alsoCourse);
                success = true;
            }
            catch (Exception ex)
            {
                var message = $"Unable to update the also course. Activity: {dto.ActivityKey}, User: {dto.WebLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }

            return success;
        }

        public Guid CreateDiscount(string activityNumber, DateTime activityEndDate, string webLogin)
        {
            var discountKey = new Guid();

            var discount = new DiscountDto
            {
                ActivityNumber = activityNumber,
                ActivityEndDate = activityEndDate,
                WebLogin = webLogin
            };

            discountKey = DiscountTasks.CreateDiscount(discount);

            return discountKey;
        }

        public async Task<bool> SendWelcomeEmail(AlsoMessageDto dto)
        {
            var success = false;

            success = await EmailTasks.SendWelcomeEmail(dto);

            return success;
        }
    }
}