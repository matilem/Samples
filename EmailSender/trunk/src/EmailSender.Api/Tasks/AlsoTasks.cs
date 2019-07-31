using Aafp.EmailSender.Api.Dtos;
using Aafp.EmailSender.Api.Helpers;
using Aafp.EmailSender.Api.Tasks.Interfaces;
using System;
using System.IO;
using System.Net.Mail;
using System.Web.Routing;
using Rotativa;
using System.Web.Mvc;

namespace Aafp.EmailSender.Api.Tasks
{
    public class AlsoTasks : IAlsoTasks
    {
        public ICustomerCorrespondenceTasks CustomerCorrespondenceTasks { get; set; }

        public bool SendTestEmail(AlsoMessageDto dto, RequestContext context)
        {
            dto.HtmlBody = RazorViewGenerator.RenderView("Also", "Testing", dto, context);
            dto.TextBody = RazorViewGenerator.RenderPlainTextView("Also", "TestingPlainText", dto, context);

            var success = EmailClient.Send(dto);

            if (success)
                foreach (var toAddress in dto.To)
                {
                    CustomerCorrespondenceTasks.LogEmail(dto.HtmlBody, toAddress, dto.Subject);
                }

            return success;
        }

        public bool SendWelcomeEmail(AlsoMessageDto dto, RequestContext context)
        {           
            if(ApplicationConfig.UseTestEmail)
            {
                dto.To.Add("mmatile@aafp.org");
                //dto.To.Add("jchou@aafp.org");
                dto.To.Add("KGottsch@aafp.org");
            }
            else
            {
                dto.To.Add(dto.CourseDirectorEmail);
                dto.To.Add(dto.CourseCoordinatorEmail);
            }

            dto.Subject = GetSubject(dto.ActivityCourseType, dto.ActivityDateDisplay, dto.ActivitySponsorName, dto.ActivityLocation);

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var root = Directory.GetCurrentDirectory();
            var courseRosterLink = root + "\\Attachments\\ALSO\\CourseRoster.xls";
            
            Attachment courseRosterAttachment = new Attachment(courseRosterLink);
            
            dto.Attachments.Clear();
            dto.Attachments.Add(courseRosterAttachment);
            
            if (dto.ActivityCourseType == "ALSO Instructor")
            {
                GetAlsoInstructorEmail(dto, root, context);
            }

            if (dto.ActivityCourseType == "ALSO Provider")
            {
                GetAlsoProviderEmail(dto, root, context);
            }
            if (dto.ActivityCourseType == "BLSO Provider")
            {
                GetBlsoProviderEmail(dto, root, context);
            }

            var success = EmailClient.Send(dto);

            if (success)
                foreach (var toAddress in dto.To)
                {
                    CustomerCorrespondenceTasks.LogEmail(dto.HtmlBody, toAddress, dto.Subject);
                }

            return success;
        }

        public bool SendLearnerInstructionsEmail(AlsoLearnerMessageDto dto, RequestContext context)
        {
            if (ApplicationConfig.UseTestEmail)
            {
                dto.To.Add("mmatile@aafp.org");
                //dto.To.Add("jchou@aafp.org");
            }
            else
            {
                dto.To.Add(dto.Email);
            }

            dto.From = "alsostaff@aafp.org";
            dto.Subject = "Learner Welcome Email";
            dto.HtmlBody = RazorViewGenerator.RenderView("Also", "AlsoLearnerEmail", dto, context);
            dto.TextBody = RazorViewGenerator.RenderPlainTextView("Also", "AlsoLearnerEmailPlainText", dto, context);
            dto.Attachments.Clear();

            var success = EmailClient.Send(dto);

            if (success)
                foreach (var toAddress in dto.To)
                {
                    CustomerCorrespondenceTasks.LogEmail(dto.HtmlBody, toAddress, dto.Subject);
                }

            return success;
        }

        public bool SendStatusChangeEmail(AlsoStatusChangeMessageDto dto, RequestContext context)
        {
            if (ApplicationConfig.UseTestEmail)
            {
                dto.To.Add("mmatile@aafp.org");
                //dto.To.Add("jchou@aafp.org");
            }
            else
            {
                dto.To.Add(dto.To[0]);
            }

            dto.From = "alsostaff@aafp.org";

            if(dto.AlsoStatus == "Instructor")
            {
                dto.Subject = "ALSO Approved Instructor Status Approval";

                //var walletCard = RazorViewGenerator.RenderView("Also", "StatusChangeApprovedInstructorWalletCards", dto, context);

                var walletCardPdf = new ViewAsPdf("StatusChangeApprovedInstructorWalletCards", dto)
                { FileName = "StatusChangeApprovedInstructorWalletCards.pdf",
                    PageSize = Rotativa.Options.Size.A4,
                    PageOrientation = Rotativa.Options.Orientation.Portrait
                };

                //var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                //fileStream.Write(byteArray, 0, byteArray.Length);
                //fileStream.Close();


                //     Attachment letterAttachment = new Attachment(walletCard);

                //dto.Attachments.Add(letterAttachment);

                dto.HtmlBody = RazorViewGenerator.RenderView("Also", "StatusChangeApprovedInstructor", dto, context);
                dto.TextBody = RazorViewGenerator.RenderPlainTextView("Also", "StatusChangeApprovedInstructorPlainText", dto, context);
            }

            if(dto.AlsoStatus == "Advisory Faculty")
            {
                dto.Subject = "ALSO Advisory Faculty Status Approval";

                var walletCard = RazorViewGenerator.RenderView("Also", "StatusChangeAdvisoryFacultyWalletCards", dto, context);

                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                var root = Directory.GetCurrentDirectory();
                var letterLink = root + "\\Attachments\\ALSO\\ALSOAdvisoryFacultyLetter.pdf";

                Attachment letterAttachment = new Attachment(letterLink);

                dto.Attachments.Clear();
                dto.Attachments.Add(letterAttachment);

                dto.HtmlBody = RazorViewGenerator.RenderView("Also", "StatusChangeAdvisoryFaculty", dto, context);
                dto.TextBody = RazorViewGenerator.RenderPlainTextView("Also", "StatusChangeAdvisoryFacultyPlainText", dto, context);
            }
            
            else
            {
                return false;
            }
            
            var success = EmailClient.Send(dto);

            if (success)
                foreach (var toAddress in dto.To)
                {
                    CustomerCorrespondenceTasks.LogEmail(dto.HtmlBody, toAddress, dto.Subject);
                }

            return success;
        }

        private string GetSubject(string courseType,  string activityDate, string sponsor, string location)
        {
            var locationDisplay = string.Empty;

            if (location != string.Empty)
            {
                locationDisplay = courseType +  " Course Approval - " + activityDate + ", " + sponsor + ", " + location;
            }
            else
            {
                locationDisplay = courseType + " Course Approval - " + activityDate + ", " + sponsor;
            }

            return locationDisplay;
        }

        private void GetAlsoInstructorEmail(AlsoMessageDto dto, string root, RequestContext context)
        {
            dto.From = "also@aafp.org";

            var alsoInstructor = root + "\\Attachments\\ALSO\\ALSOInstructorInstructions.pdf";
            var orderFormLink = root + "\\Attachments\\ALSO\\ALSOInstructorOrderForm.pdf";

            Attachment intructionsAttachment = new Attachment(alsoInstructor);
            Attachment orderFormAttachment = new Attachment(orderFormLink);

            dto.Attachments.Add(intructionsAttachment);
            dto.Attachments.Add(orderFormAttachment);

            dto.HtmlBody = RazorViewGenerator.RenderView("Also", "InstructorWelcome", dto, context);
            dto.TextBody = RazorViewGenerator.RenderPlainTextView("Also", "InstructorWelcomePlainText", dto, context);
        }

        private void GetAlsoProviderEmail(AlsoMessageDto dto, string root, RequestContext context)
        {
            dto.From = "also@aafp.org";

            var alsoProvider = root + "\\Attachments\\ALSO\\ALSOProviderInstructions.pdf";
            var orderFormLink = root + "\\Attachments\\ALSO\\ALSOProviderOrderForm.pdf";

            Attachment intructionsAttachment = new Attachment(alsoProvider);
            Attachment orderFormAttachment = new Attachment(orderFormLink);

            dto.Attachments.Add(intructionsAttachment);
            dto.Attachments.Add(orderFormAttachment);

            dto.HtmlBody = RazorViewGenerator.RenderView("Also", "AlsoProviderWelcome", dto, context);
            dto.TextBody = RazorViewGenerator.RenderPlainTextView("Also", "AlsoProviderWelcomePlainText", dto, context);
        }

        private void GetBlsoProviderEmail(AlsoMessageDto dto, string root, RequestContext context)
        {
            dto.From = "blso@aafp.org";

            var blsoProvider = root + "\\Attachments\\ALSO\\BLSOProviderInstructions.pdf";
            var orderFormLink = root + "\\Attachments\\ALSO\\BLSOProviderOrderForm.pdf";

            Attachment intructionsAttachment = new Attachment(blsoProvider);
            Attachment orderFormAttachment = new Attachment(orderFormLink);

            dto.Attachments.Add(intructionsAttachment);
            dto.Attachments.Add(orderFormAttachment);

            dto.HtmlBody = RazorViewGenerator.RenderView("Also", "BlsoProviderWelcome", dto, context);
            dto.TextBody = RazorViewGenerator.RenderPlainTextView("Also", "BlsoProviderWelcomePlainText", dto, context);
        }
    }
}