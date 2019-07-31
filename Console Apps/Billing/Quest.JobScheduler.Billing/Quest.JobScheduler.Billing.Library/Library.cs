using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Quest.JobScheduler.Billing.Repo;
using ExamCentral.Core.Interfaces;
using ExamCentral.Core.Logging;

namespace Quest.JobScheduler.Billing.Library
{
    public class Process
    {
        public void Execute()
        {
            ILogger logger = new Logger();
            
            try
            {
                new DB().p_billingload(DateTime.Now);

                new DB().p_billinghistory();

                //Populate Billing table
                //Get Billing Info
                //Save Billing to History table

            }

            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Database Processing Exception: {0}, {1}", ex.Message, ex.StackTrace);
                logger.LogToQueue(XMLData.LoggingLevel.Error, ConfigurationManager.AppSettings["ErrorQueue"], new ErrorXMLData { ApplicationName = "Quest.JobScheduler.Billing", LineofBusiness = "Job Scheduler", Method = "Process.Execute", ErrorMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}
