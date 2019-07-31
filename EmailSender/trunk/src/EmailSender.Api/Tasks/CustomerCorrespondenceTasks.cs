using System;
using Aafp.EmailSender.Api.Daos.Commands.Interfaces;
using Aafp.EmailSender.Api.Helpers;
using Aafp.EmailSender.Api.Models;
using Aafp.EmailSender.Api.Tasks.Interfaces;

namespace Aafp.EmailSender.Api.Tasks
{
    public class CustomerCorrespondenceTasks : ICustomerCorrespondenceTasks
    {
        public ICustomerCorrespondenceCommand CustomerCorrespondenceCommand { get; set; }

        public IIndividualTasks IndividualTasks { get; set; }

        public async void LogEmail(string content, string recipient, string subject)
        {
            var individual = await IndividualTasks.GetByEmail(recipient);

            if (individual != null)
            {
                try
                {
                    var customerCorrespondence = new CustomerCorrespondence();
                    customerCorrespondence.CustomerKey = individual.Key;
                    customerCorrespondence.CommunicationMethod = "E-mail";
                    customerCorrespondence.CorrespondenceContent = content;
                    customerCorrespondence.CorrespondenceDate = DateTime.Now;
                    customerCorrespondence.CorrespondenceType = "Confirmation Sent";
                    customerCorrespondence.CommunicationValue = recipient;
                    customerCorrespondence.AddUser = "WebUpdate";
                    customerCorrespondence.AddDate = DateTime.Now;
                    customerCorrespondence.CallToActionKey = null;
                    customerCorrespondence.CallToActionObjectKey = null;
                    customerCorrespondence.CommunicationDescription = subject;
                    customerCorrespondence.CorrespondenceSubject = subject;

                    CustomerCorrespondenceCommand.Store(customerCorrespondence);
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error logging email {subject} to {recipient}. Error: {ex.Message}");
                }
            }
        }
    }
}