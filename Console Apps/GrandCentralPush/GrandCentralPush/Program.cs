using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GrandCentralPush
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime Start = DateTime.Now;
            // define objects needed for data retrieval
            List<Data.Data> data = null;
            Business.Business mlBiz = new GrandCentralPush.Business.Business();

            Console.WriteLine("Objects Defined" + " " + DateTime.Now.Subtract(Start).ToString());

            // define objects needed for auditing
            GrandCentralPush.Logs.AuditTrail audit = new GrandCentralPush.Logs.AuditTrail(mlBiz.StartDate, mlBiz.EndDate);
            GrandCentralPush.Logs.LastRun lastRun = new GrandCentralPush.Logs.LastRun(mlBiz.StartDate, mlBiz.EndDate);

            try
            {
                Console.WriteLine("Audit Started" + " " + DateTime.Now.Subtract(Start).ToString());

                audit.WriteLine(String.Format("Beginning Process for: {0}", mlBiz.StartDate.ToString("MM/dd/yyyy hh:mm")));
                audit.WriteLine("-------------------------------------------");
                audit.WriteLine("Gathering Data");

                Console.WriteLine("Running Stored Proc" + " " + DateTime.Now.Subtract(Start).ToString());
                // get data
                data = mlBiz.ReadByDateRange();

                Console.WriteLine("Before If Statement" + " " + DateTime.Now.Subtract(Start).ToString());
                // if valid data exists:
                if (data.Count > 0)
                {
                    Console.WriteLine("If Statement" + " " + DateTime.Now.Subtract(Start).ToString());
                    
                    audit.WriteLine(String.Format("Current Data exists: Processing {0} rows.", data.Count.ToString()));

                    Console.WriteLine("CSVDataStream" + " " + DateTime.Now.Subtract(Start).ToString());
                    CSV.CSVDataStreamBuilder stream = new GrandCentralPush.CSV.CSVDataStreamBuilder();
                    string StreamData = stream.BuildDataList(data);

                    Console.WriteLine("Create CSV" + " " + DateTime.Now.Subtract(Start).ToString());
                    audit.WriteLine("Attempting to create CSV file");

                    // need file name with path;
                    string DataListFileName = string.Concat(ConfigurationManager.AppSettings["CSVFilePath"], "EXM1_ALLORDERS_METM_", mlBiz.EndDate.ToString("yyyyMMddHHmmss_"), data.Count.ToString(), ".csv");

                    Console.WriteLine("CSV Created" + " " + " " + DateTime.Now.Subtract(Start).ToString());
                    audit.WriteLine("CSV file created");

                    CSV.CSVFile file = new GrandCentralPush.CSV.CSVFile();
                    file.Save(StreamData, DataListFileName);

                    Console.WriteLine("Send to XMIT" + " " + DateTime.Now.Subtract(Start).ToString());
                    audit.WriteLine(String.Format("File: {0} has been successfully sent to XMIT", DataListFileName));

                    // Update last run file for next use and exit
                    Console.WriteLine("Update LastRun" + " " + DateTime.Now.Subtract(Start).ToString());
                    lastRun.Update();
                }
                else
                {
                    Console.WriteLine("Else Statement" + " " + DateTime.Now.Subtract(Start).ToString());
                    
                    // Update audit file
                    audit.WriteLine("No Current Data Exists - Sending Blank File");

                    CSV.CSVDataStreamBuilder stream = new GrandCentralPush.CSV.CSVDataStreamBuilder();
                    string StreamData = stream.BuildDataList(data);

                    Console.WriteLine("CSV File" + " " + DateTime.Now.Subtract(Start).ToString());
                    audit.WriteLine("Attempting to create CSV file");

                    string DataListFileName = string.Concat(ConfigurationManager.AppSettings["CSVFilePath"], "EXM1_ALLORDERS_METM_", mlBiz.EndDate.ToString("yyyyMMddHHmmss_"), data.Count.ToString(), ".csv");

                    Console.WriteLine("CSV Created" + " " + DateTime.Now.Subtract(Start).ToString());
                    audit.WriteLine("CSV file created");

                    CSV.CSVFile file = new GrandCentralPush.CSV.CSVFile();
                    file.Save(StreamData, DataListFileName);

                    Console.WriteLine("Send to XMIT" + " " + DateTime.Now.Subtract(Start).ToString());
                    audit.WriteLine(String.Format("File: {0} has been successfully sent to XMIT", DataListFileName));

                    Console.WriteLine("Update LastRun" + " " + DateTime.Now.Subtract(Start).ToString());
                    lastRun.Update();
                }
                audit.WriteLine("Process Completed Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + " " + DateTime.Now.Subtract(Start).ToString());
                audit.WriteLine("An error occurred and processing may not have completed successfully. Please check the error log for details");
                GrandCentralPush.Logs.Error error = new GrandCentralPush.Logs.Error(mlBiz.StartDate, mlBiz.EndDate);
                error.WriteLine(ex.Message.ToString());
                error.WriteLine(ex.StackTrace.ToString());
                error.WriteToFile();
            }
            finally
            {
                Console.WriteLine("Write File" + " " + DateTime.Now.Subtract(Start).ToString());
                audit.WriteToFile();
            }

            try
            {
                Console.WriteLine("Delete Files" + " " + DateTime.Now.Subtract(Start).ToString());
                //Delete files older than 30 days from all folders.
                string dircsv = ConfigurationManager.AppSettings["CSVFilePath"];
                string diraudit = ConfigurationManager.AppSettings["AuditFilePath"];
                string direrror = ConfigurationManager.AppSettings["ErrorFilePath"];

                string[] csvfile = Directory.GetFiles(dircsv);
                string[] auditfile = Directory.GetFiles(diraudit);
                string[] errorfile = Directory.GetFiles(direrror);

                Console.WriteLine("Delete CSV" + " " + DateTime.Now.Subtract(Start).ToString());
                foreach (string csvname in csvfile)
                {
                    FileInfo fi = new FileInfo(csvname);
                    if (fi.CreationTime < DateTime.Now.AddDays(-30))

                        File.Delete(csvname);
                }

                Console.WriteLine("Delete Audit" + " " + DateTime.Now.Subtract(Start).ToString());
                foreach (string auditname in auditfile)
                {
                    FileInfo fi = new FileInfo(auditname);
                    if (fi.CreationTime < DateTime.Now.AddDays(-30))

                        File.Delete(auditname);
                }

                Console.WriteLine("Delete Error" + " " + DateTime.Now.Subtract(Start).ToString());
                foreach (string errorname in errorfile)
                {
                    FileInfo fi = new FileInfo(errorname);
                    if (fi.CreationTime < DateTime.Now.AddDays(-30))

                        File.Delete(errorname);
                }
                Console.WriteLine("Victory!!!!!!!" + " " + DateTime.Now.Subtract(Start).ToString());
                //Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + " " + DateTime.Now.Subtract(Start).ToString());
                audit.WriteLine("An error occurred and processing may not have completed successfully. Please check the error log for details");
                GrandCentralPush.Logs.Error error = new GrandCentralPush.Logs.Error(mlBiz.StartDate, mlBiz.EndDate);
                error.WriteLine(ex.Message.ToString());
                error.WriteLine(ex.StackTrace.ToString());
                error.WriteToFile();
            }

            finally
            {
                Console.WriteLine("All Files Deleted." + " " + DateTime.Now.Subtract(Start).ToString());
            }

        }
    }
}

