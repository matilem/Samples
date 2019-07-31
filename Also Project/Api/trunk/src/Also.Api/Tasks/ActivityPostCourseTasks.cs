using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Helpers;
using Aafp.Also.Api.Models;
using Aafp.Also.Api.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks
{
    public class ActivityPostCourseTasks : IActivityPostCourseTasks
    {
        public IIndividualTasks IndividualTasks { get; set; }

        public ICmeTasks CmeTasks { get; set; }

        public IAlsoStatusTasks AlsoStatusTasks { get; set; }

        public IActivityPostCourseQuery ActivityPostCourseQuery { get; set; }

        public IActivityQuery ActivityQuery { get; set; }

        public IAlsoCourseQuery AlsoCourseQuery { get; set; }

        public ILearnerOccupationsQuery LearnerOccupationsQuery { get; set; }

        public IAlsoStatusQuery AlsoStatusQuery { get; set; }

        public IAlsoCourseCommand AlsoCourseCommand { get; set; }

        public ILearnerCommand LearnerCommand { get; set; }

        public IInstructorCommand InstructorCommand { get; set; }

        public async Task<ActivityPostCourseDto> GetPostCourse(string activityNumber, string webLogin)
        {
            var dto = new ActivityPostCourseDto();
            dto.AlsoCourse = new AlsoCourseDto();
            dto.Learners = new List<ActivityPostCourseLearnerDto>();
            dto.LearnerOccupations = new List<LearnerOccupationDto>();
            dto.Instructors = new List<ActivityPostCourseInstructorDto>();

            dto.Customer = await IndividualTasks.GetIndividualByWebLogin(webLogin);

            dto.Activity = ActivityQuery.GetActivity(activityNumber);
            dto.Learners = ActivityPostCourseQuery.GetActivityLearners(activityNumber);
            dto.LearnerOccupations = LearnerOccupationsQuery.GetLearnerOccupations();
            dto.AlsoCourse = AlsoCourseQuery.GetAlsoCourse(dto.Activity.ActivityKey);
            dto.Instructors = ActivityPostCourseQuery.GetActivityInstructors(dto.Activity.ActivityKey);

            foreach (var learner in dto.Learners)
            {
                learner.Eligible = ActivityPostCourseQuery.VerifyEligibility(learner.CustomerKey, dto.Activity.ActivityCourseType);
            }

            foreach (var instructor in dto.Instructors)
            {
                var statuses = AlsoStatusQuery.GetAlsoStatuses(instructor.CustomerKey);
                instructor.CurrentAlsoStatus = CurrentAlsoStatus(statuses);
            }

            return dto;
        }

        public async Task<bool> SavePostCourse(ActivityPostCourseSubmissionDto dto)
        {
            var success = false;
            var passingLearners = new List<Guid>();
            var failingLearners = new List<Guid>();
            var instructorKeys = new List<Guid>();
            var alsoCourse = new AlsoCourse();
            var alsoCourseHistory = new AlsoCourseHistoryDto();

            if (dto.AlsoCourseKey == Guid.Empty)
            {
                success = AddAlsoCourse(dto);
            }
            else
            {
                alsoCourse = AlsoCourseCommand.GetByKey(dto.AlsoCourseKey);
                success = UpdateAlsoCourse(alsoCourse, dto.WebLogin, dto.Status);
            }

            if (dto.Learners != null)
            {
                foreach (var learner in dto.Learners)
                {
                    if (learner.LearnerKey != Guid.Empty)
                    {
                        var existingLearner = LearnerCommand.GetByKey(learner.LearnerKey);

                        success = UpdateLearner(existingLearner, learner, dto.WebLogin, dto.AlsoCourseKey);
                    }
                    else
                    {
                        success = AddLearner(learner, dto.WebLogin, dto.AlsoCourseKey);
                    }

                    if (learner.Passed)
                    {
                        passingLearners.Add(learner.CustomerKey);
                    }
                    if (learner.Failed)
                    {
                        failingLearners.Add(learner.CustomerKey);
                    }
                }
            }

            if (dto.Instructors != null)
            {
                foreach (var instructor in dto.Instructors)
                {
                    instructorKeys.Add(instructor.InstructorKey);

                    if (instructor.InstructorKey != Guid.Empty)
                    {
                        var existingInstructor = InstructorCommand.GetByKey(instructor.InstructorKey);

                        success = UpdateInstructor(existingInstructor, instructor, dto.WebLogin, dto.AlsoCourseKey, dto.ActivityKey);
                    }
                    else
                    {
                        success = AddInstructor(instructor, dto.WebLogin, dto.AlsoCourseKey, dto.ActivityKey);
                    }
                }

                if (dto.Status == "Complete")
                {
                    //Learners
                    success = await ReportAlsoCredit(dto.WebLogin, dto.ActivityNumber, passingLearners);
                    success = await ReportAlsoCredit(dto.WebLogin, dto.ActivityNumber, failingLearners);

                    success = ReportAlsoCourseHistory(dto.ActivityNumber, passingLearners, dto.WebLogin, dto.ActivityCourseType);

                    //Instructors
                    success = ReportAlsoCourseHistory(dto.ActivityNumber, instructorKeys, dto.WebLogin, dto.ActivityCourseType);
                    success = await ReportTeachingCredit(dto.WebLogin, dto.ActivityNumber, instructorKeys);
                    success = await UpdateAlsoStatus(dto.ActivityNumber, instructorKeys, dto.WebLogin, dto.ActivityCourseType);
                }
            }

            return success;
        }

        public bool AddAlsoCourse(ActivityPostCourseSubmissionDto dto)
        {
            var success = false;
            try
            {
                var alsoCourse = new AlsoCourse
                {
                    ActivityKey = dto.ActivityKey,
                    AddDate = DateTime.Now,
                    AddUser = dto.WebLogin
                };

                if (dto.Status == "Submit")
                {
                    alsoCourse.PostCourseSubmittedFlag = true;
                }

                if (dto.Status == "Complete")
                {
                    alsoCourse.PostCourseSubmittedFlag = true;
                    alsoCourse.PostCourseCompletedFlag = true;
                }

                AlsoCourseCommand.Store(alsoCourse);
                success = true;
            }

            catch (Exception ex)
            {
                var message = $"Unable to add the Also Course  Activity: {dto.ActivityKey}, User: {dto.WebLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }
            return success;
        }

        public bool UpdateAlsoCourse(AlsoCourse alsoCourse, string WebLogin, string status)
        {
            var success = false;
            try
            {
                alsoCourse.ChangeDate = DateTime.Now;
                alsoCourse.ChangeUser = WebLogin;                

                if (status == "Submit")
                {
                    alsoCourse.PostCourseSubmittedFlag = true;
                }

                if (status == "Complete")
                {
                    alsoCourse.PostCourseSubmittedFlag = true;
                    alsoCourse.PostCourseCompletedFlag = true;
                }

                AlsoCourseCommand.Store(alsoCourse);
                success = true;
            }

            catch (Exception ex)
            {
                var message = $"Unable to add the Also Course, Activity: {alsoCourse.ActivityKey}, User: {WebLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }
            return success;
        }

        public bool AddLearner(LearnerSubmissionDto dto, string webLogin, Guid alsoCourseKey)
        {
            var success = false;
            try
            {
                var learner = new Learner
                {
                    CustomerKey = dto.CustomerKey,
                    OccupationKey = dto.OccupationKey,
                    AlsoCourseKey = alsoCourseKey,
                    AddDate = DateTime.Now,
                    AddUser = webLogin,
                    PassedFlag = dto.Passed,
                    FailedFlag = dto.Failed,
                    NoShowFlag = dto.NoShow
                };

                LearnerCommand.Store(learner);
                success = true;
            }

            catch (Exception ex)
            {
                var message = $"Unable to add the Learner. Course: {alsoCourseKey}, User: {webLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }
            return success;
        }

        public bool UpdateLearner(Learner existingLearner, LearnerSubmissionDto dto, string webLogin, Guid alsoCourseKey)
        {
            var success = false;

            try
            {
                existingLearner.OccupationKey = dto.OccupationKey;
                existingLearner.PassedFlag = dto.Passed;
                existingLearner.FailedFlag = dto.Failed;
                existingLearner.NoShowFlag = dto.NoShow;
                existingLearner.ChangeDate = DateTime.Now;
                existingLearner.ChangeUser = webLogin;

                LearnerCommand.Store(existingLearner);
                success = true;
            }
            catch (Exception ex)
            {
                var message = $"Unable to update the Learner. Course: {alsoCourseKey}, User: {webLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }

            return success;
        }

        public bool AddInstructor(InstructorSubmissionDto dto, string webLogin, Guid alsoCourseKey, Guid activityKey)
        {
            var success = false;
            try
            {
                var instructor = new Instructor
                {
                    CustomerKey = dto.CustomerKey,
                    AlsoCourseKey = alsoCourseKey,
                    ActivityKey = activityKey,
                    AddDate = DateTime.Now,
                    AddUser = webLogin,
                    AdvisoryFacultyRecommended = dto.AdvisoryFacultyRecommended,
                    InstructorRecommended = dto.InstructorRecommended
                };

                InstructorCommand.Store(instructor);
                success = true;
            }

            catch (Exception ex)
            {
                var message = $"Unable to add the Instructor. Course: {alsoCourseKey}, User: {webLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }
            return success;

        }

        public bool UpdateInstructor(Instructor existingInstructor, InstructorSubmissionDto dto, string webLogin, Guid alsoCourseKey, Guid activityKey)
        {
            var success = false;

            try
            {
                existingInstructor.CustomerKey = dto.CustomerKey;
                existingInstructor.AlsoCourseKey = alsoCourseKey;
                existingInstructor.ActivityKey = activityKey;
                existingInstructor.AdvisoryFacultyRecommended = dto.AdvisoryFacultyRecommended;
                existingInstructor.InstructorRecommended = dto.InstructorRecommended;
                existingInstructor.ChangeDate = DateTime.Now;
                existingInstructor.ChangeUser = webLogin;

                InstructorCommand.Store(existingInstructor);
                success = true;
            }
            catch (Exception ex)
            {
                var message = $"Unable to update the Instructor. Course: {alsoCourseKey}, User: {webLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }

            return success;
        }

        public async Task<bool> ReportAlsoCredit(string webLogin, string activityNumber, List<Guid> learners)
        {
            var success = false;
            var dto = new AlsoCreditDto();
            var activity = ActivityQuery.GetActivity(activityNumber);

            dto.WebLogin = webLogin;
            dto.SessionKey = activity.ActivitySessionKey;
            dto.CustomerKeys = learners;

            success = await CmeTasks.ReportCmeForAlso(dto);

            return success;
        }

        private string CurrentAlsoStatus(List<AlsoStatusDto> alsoStatuses)
        {
            var display = string.Empty;

            foreach (var status in alsoStatuses)
            {
                if (DateTime.Now >= status.ExpirationDate)
                {
                    if (status.AlsoStatusType == "Advisory Faculty")
                    {
                        return status.AlsoStatusType;
                    }
                    if (status.AlsoStatusType == "Instructor")
                    {
                        return status.AlsoStatusType;
                    }
                    if (status.AlsoStatusType == "Instructor Candidate")
                    {
                        return status.AlsoStatusType;
                    }
                    if (status.AlsoStatusType == "Provider")
                    {
                        return status.AlsoStatusType;
                    }
                }
            }

            return display;
        }

        public bool ReportAlsoCourseHistory(string activityNumber, List<Guid> customerKeys, string webLogin, string courseType)
        {
            var success = false;
            var alsoCourseHistory = new AlsoCourseHistoryDto();
            var activity = ActivityQuery.GetActivity(activityNumber);

            alsoCourseHistory.SessionKey = activity.ActivitySessionKey;

            if (courseType == "ALSO Provider")
            {
                alsoCourseHistory.Role = 'P';
            }

            if (courseType == "BLSO Provider")
            {
                alsoCourseHistory.Role = 'P';
            }

            if (courseType == "ALSO Instructor")
            {
                alsoCourseHistory.Role = 'I';
            }

            foreach (var cstKey in customerKeys)
            {
                alsoCourseHistory.CustomerKey = cstKey;
                success = AlsoStatusTasks.SaveAlsoCourseHistory(alsoCourseHistory, webLogin);
            }

            return success;
        }

        public async Task<bool> ReportTeachingCredit(string webLogin, string activityNumber, List<Guid> instructors)
        {
            var success = false;
            var dto = new TeachingCreditDto();
            var activity = ActivityQuery.GetActivity(activityNumber);

            dto.WebLogin = webLogin;
            dto.SessionKey = activity.ActivitySessionKey;
            dto.CustomerKeys = instructors;

            success = await CmeTasks.ReportTeachingCredits(dto);

            return success;
        }

        public async Task<bool> UpdateAlsoStatus(string activityNumber, List<Guid> customerKeys, string webLogin, string courseType)
        {
            var success = false;

            foreach (var cstKey in customerKeys)
            {
                success = await AlsoStatusTasks.UpdateInstructorStatus(cstKey, courseType, webLogin);
            }

            return success;
        }
    }
}