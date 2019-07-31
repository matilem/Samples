using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Helpers;
using Aafp.Also.Api.Models;
using Aafp.Also.Api.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aafp.Also.Api.Tasks
{
    public class AlsoStatusTasks : IAlsoStatusTasks
    {
        public IAlsoCourseHistoryCommand AlsoCourseHistoryCommand { get; set; }

        public IAlsoStatusCommand AlsoStatusCommand { get; set; }

        public IAlsoStatusQuery AlsoStatusQuery { get; set; }

        public IIndividualTasks IndividualTasks { get; set; }

        public IEmailTasks EmailTasks { get; set; }

        public bool SaveAlsoCourseHistory(AlsoCourseHistoryDto dto, string webLogin)
        {
            var success = false;
            try
            {
                var alsoCourseHistory = new AlsoCourseHistory
                {
                    CustomerKey = dto.CustomerKey,
                    SessionKey = dto.SessionKey,
                    Role = dto.Role,
                    AddDate = DateTime.Now,
                    AddUser = webLogin,
                };

                AlsoCourseHistoryCommand.Store(alsoCourseHistory);
                success = true;
            }

            catch (Exception ex)
            {
                var message = $"Unable to add the Also Course History. SessionKey: {dto.SessionKey}, User: {webLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }
            return success;
        }

        public async Task<bool> UpdateInstructorStatus(Guid CustomerKey, string alsoCourseType, string webLogin)
        {
            var success = false;

            var alsoStatuses = new List<AlsoStatusDto>();
            var allStatuses = new List<AlsoStatusTypeDto>();
            var alsoStatus = new AlsoStatus();
            var expirationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddYears(2).AddMonths(1).AddDays(-1);
            var teachingCredits = 0;

            var alsoCourses = AlsoStatusQuery.GetAlsoCourseHistory(CustomerKey);
            var currentAlsoStatus = IndividualTasks.CurrentAlsoStatus(alsoStatuses);

            try
            {
                alsoStatuses = IndividualTasks.GetAlsoStatuses(CustomerKey);
                allStatuses = AlsoStatusQuery.GetAlsoStatusTypes();

                //ALSO Provider
                // Take a provider course every two years to end of month
                if (currentAlsoStatus == "Provider")
                {
                    teachingCredits = AlsoStatusQuery.GetCmeTeachingCreditsCount(CustomerKey, -2);

                    if (teachingCredits >= 2)
                    {
                        var currentStatus = alsoStatuses.Where(x => x.AlsoStatusType == "Provider").FirstOrDefault();
                        var status = AlsoStatusCommand.GetByKey(currentStatus.AlsoStatusKey);

                        status.StartDate = DateTime.Now;
                        status.ExpirationDate = expirationDate;
                        status.ChangeDate = DateTime.Now;
                        status.ChangeUser = webLogin;

                        AlsoStatusCommand.Store(status);
                    }
                }

                //BLSO Provider
                if (currentAlsoStatus == "BLSO Provider")
                {
                    teachingCredits = AlsoStatusQuery.GetCmeTeachingCreditsCount(CustomerKey, -2);

                    if (teachingCredits >= 2)
                    {
                        var currentStatus = alsoStatuses.Where(x => x.AlsoStatusType == "BLSO Provider").FirstOrDefault();
                        var status = AlsoStatusCommand.GetByKey(currentStatus.AlsoStatusKey);

                        status.StartDate = DateTime.Now;
                        status.ExpirationDate = expirationDate;
                        status.ChangeDate = DateTime.Now;
                        status.ChangeUser = webLogin;

                        AlsoStatusCommand.Store(status);
                    }
                }

                //Instructor Candidate
                // Take a provider course and and instructor course
                // Instructor Candidate, teach once in a year of instructor course date

                if (currentAlsoStatus == "Instructor Candidate")
                {
                    teachingCredits = AlsoStatusQuery.GetCmeTeachingCreditsCount(CustomerKey, -5);
                    var providerCourse = alsoCourses.Where(x => x.AlsoStatusRole == 'P').Count();
                    var instructorCourse = alsoCourses.Where(x => x.ActivityCourseType == "Instructor Course").Count();

                    if (teachingCredits >= 1 && providerCourse >= 1 && instructorCourse >= 1)
                    {
                        //Move to Instructor - Update Provider, Instructor Candidate, Insert Instructor
                        var alsoStatusTypeKey = allStatuses.FirstOrDefault(x => x.AlsoStatusType == "Instructor").AlsoStatusTypeKey;
                        var currentProviderStatus = alsoStatuses.Where(x => x.AlsoStatusType == "Provider" || x.AlsoStatusType == "BLSO Provider").FirstOrDefault();
                        var providerStatus = AlsoStatusCommand.GetByKey(currentProviderStatus.AlsoStatusKey);

                        providerStatus.StartDate = DateTime.Now;
                        providerStatus.ExpirationDate = expirationDate;
                        providerStatus.ChangeDate = DateTime.Now;
                        providerStatus.ChangeUser = webLogin;

                        AlsoStatusCommand.Store(providerStatus);

                        var currentInstructorCandidateStatus = alsoStatuses.Where(x => x.AlsoStatusType == "Instructor Candidate").FirstOrDefault();
                        var instructorCandidateStatus = AlsoStatusCommand.GetByKey(currentInstructorCandidateStatus.AlsoStatusKey);

                        //instructorCandidateStatus.StartDate = DateTime.Now;
                        //instructorCandidateStatus.ExpirationDate = expirationDate;
                        instructorCandidateStatus.ApproveDate = DateTime.Now;
                        instructorCandidateStatus.ChangeDate = DateTime.Now;
                        instructorCandidateStatus.ChangeUser = webLogin;

                        AlsoStatusCommand.Store(instructorCandidateStatus);

                        var newInstructorStatus = new AlsoStatus
                        {
                            AddDate = DateTime.Now,
                            AddUser = webLogin,
                            CustomerKey = CustomerKey,
                            StartDate = DateTime.Now,
                            ExpirationDate = expirationDate,
                            AlsoStatusTypeKey = alsoStatusTypeKey
                        };

                        AlsoStatusCommand.Store(newInstructorStatus);
                        success = true;

                        //Send Status Change Email
                        var learner = await IndividualTasks.GetIndividualByCustomerKey(CustomerKey);
                        var statusChangeEmail = new AlsoStatusChangeMessageDto
                        {
                            AlsoStatus = "Instructor",
                            StartDate = newInstructorStatus.StartDate,
                            ExpirationDate = newInstructorStatus.ExpirationDate,
                            FirstName = learner.FirstName,
                            LastName = learner.LastName,
                            Designation = learner.Designation,
                            Email = learner.Email
                        };

                        success = await EmailTasks.SendStatusChangeEmail(statusChangeEmail);
                    }
                }

                //Instructor
                // To maintain 3 teaching credits within 5 years

                if (currentAlsoStatus == "Instructor")
                {
                    teachingCredits = AlsoStatusQuery.GetCmeTeachingCreditsCount(CustomerKey, -5);
                    var advisoryFacultyRecommendation = AlsoStatusQuery.GetAdvisoryFacultyRecommendation(CustomerKey);

                    if (teachingCredits >= 3)
                    {
                        foreach (var status in alsoStatuses.Where(x => x.AlsoStatusType != "Instructor Candidate"))
                        {
                            var existingStatus = AlsoStatusCommand.GetByKey(status.AlsoStatusKey);

                            existingStatus.ChangeDate = DateTime.Now;
                            existingStatus.ChangeUser = webLogin;
                            existingStatus.StartDate = DateTime.Now;
                            existingStatus.ExpirationDate = expirationDate;

                            AlsoStatusCommand.Store(existingStatus);
                            success = true;
                        }
                    }

                    if (teachingCredits >= 3 && advisoryFacultyRecommendation)
                    {
                        var alsoStatusTypeKey = allStatuses.FirstOrDefault(x => x.AlsoStatusType == "Advisory Faculty").AlsoStatusTypeKey;

                        var newAdvisoryFacultyStatus = new AlsoStatus
                        {
                            AddDate = DateTime.Now,
                            AddUser = webLogin,
                            CustomerKey = CustomerKey,
                            StartDate = DateTime.Now,
                            ExpirationDate = expirationDate,
                            AlsoStatusTypeKey = alsoStatusTypeKey
                        };

                        AlsoStatusCommand.Store(newAdvisoryFacultyStatus);
                        success = true;

                        //Send Email now Advisory Faculty
                        var learner = await IndividualTasks.GetIndividualByCustomerKey(CustomerKey);
                        var statusChangeEmail = new AlsoStatusChangeMessageDto
                        {
                            AlsoStatus = "Advisory Faculty",
                            StartDate = newAdvisoryFacultyStatus.StartDate,
                            ExpirationDate = newAdvisoryFacultyStatus.ExpirationDate,
                            FirstName = learner.FirstName,
                            LastName = learner.LastName,
                            Designation = learner.Designation,
                            Email = learner.Email
                        };

                        success = await EmailTasks.SendStatusChangeEmail(statusChangeEmail);
                    }


                    //Advisory Faculty
                    // Recommendation from Advisory Faculty
                    // To maintain 3 teaching credits withing 5 years

                    if (currentAlsoStatus == "Advisory Faculty")
                    {
                        teachingCredits = AlsoStatusQuery.GetCmeTeachingCreditsCount(CustomerKey, -5);

                        if (teachingCredits >= 3)
                        {
                            foreach (var status in alsoStatuses.Where(x => x.AlsoStatusType != "Advisory Faculty"))
                            {
                                var existingStatus = AlsoStatusCommand.GetByKey(status.AlsoStatusKey);

                                existingStatus.ChangeDate = DateTime.Now;
                                existingStatus.ChangeUser = webLogin;
                                existingStatus.StartDate = DateTime.Now;
                                existingStatus.ExpirationDate = expirationDate;

                                AlsoStatusCommand.Store(existingStatus);
                                success = true;
                            }
                        }
                    }

                    else
                    {
                        if (alsoCourseType == "ALSO Provider")
                        {
                            var currentStatus = alsoStatuses.Where(x => x.AlsoStatusType == "Provider").FirstOrDefault();
                            if (currentStatus != null)
                            {
                                var providerStatus = AlsoStatusCommand.GetByKey(currentStatus.AlsoStatusKey);
                                providerStatus.StartDate = DateTime.Now;
                                providerStatus.ExpirationDate = expirationDate;
                                providerStatus.ChangeDate = DateTime.Now;
                                providerStatus.ChangeUser = webLogin;

                                AlsoStatusCommand.Store(providerStatus);
                            }
                            else
                            {
                                var alsoStatusTypeKey = allStatuses.FirstOrDefault(x => x.AlsoStatusType == "Provider").AlsoStatusTypeKey;
                                alsoStatus = new AlsoStatus
                                {
                                    AddDate = DateTime.Now,
                                    AddUser = webLogin,
                                    CustomerKey = CustomerKey,
                                    StartDate = DateTime.Now,
                                    ExpirationDate = expirationDate,
                                    AlsoStatusTypeKey = alsoStatusTypeKey
                                };
                                AlsoStatusCommand.Store(alsoStatus);
                            }
                        }
                        if (alsoCourseType == "BLSO Provider")
                        {
                            var currentStatus = alsoStatuses.Where(x => x.AlsoStatusType == "BLSO Provider").FirstOrDefault();
                            if (currentStatus != null)
                            {
                                var providerStatus = AlsoStatusCommand.GetByKey(currentStatus.AlsoStatusKey);
                                providerStatus.StartDate = DateTime.Now;
                                providerStatus.ExpirationDate = expirationDate;
                                providerStatus.ChangeDate = DateTime.Now;
                                providerStatus.ChangeUser = webLogin;

                                AlsoStatusCommand.Store(providerStatus);
                            }
                            else
                            {
                                var alsoStatusTypeKey = allStatuses.FirstOrDefault(x => x.AlsoStatusType == "BLSO Provider").AlsoStatusTypeKey;
                                alsoStatus = new AlsoStatus
                                {
                                    AddDate = DateTime.Now,
                                    AddUser = webLogin,
                                    CustomerKey = CustomerKey,
                                    StartDate = DateTime.Now,
                                    ExpirationDate = expirationDate,
                                    AlsoStatusTypeKey = alsoStatusTypeKey
                                };

                                AlsoStatusCommand.Store(alsoStatus);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                var message = $"Unable to change also status. CustomerKey: {CustomerKey}, User: {webLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }
            return success;
        }
    }
}