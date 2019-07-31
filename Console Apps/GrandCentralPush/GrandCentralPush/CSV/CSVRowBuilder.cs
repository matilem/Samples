using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GrandCentralPush.CSV
{
    class CSVRowBuilder
    {
        private const char delim = '\t';

        public string BuildDataRow(Data.Data data)
        {
            // add quotes to all datafields to allow commas within data
            // to become standard text, rather than acting as delimiters
            string fData = String.Empty;
            fData = string.Concat(fData, "\"" +  data.AppLastName + "\"" , delim);
            fData = string.Concat(fData, "\"" + data.AppFirstName + "\"", delim);
            fData = string.Concat(fData, "\"" + data.HomeAddress1 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.HomeAddress2 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.HomeAddress3 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.HomeCity + "\"", delim);
            fData = string.Concat(fData, "\"" + data.HomeState + "\"", delim);
            fData = string.Concat(fData, "\"" + data.HomeZip + "\"", delim);
            fData = string.Concat(fData, "\"" + data.WorkAddress1 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.WorkAddress2 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.WorkAddress3 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.WorkCity + "\"", delim);
            fData = string.Concat(fData, "\"" + data.WorkState + "\"", delim);
            fData = string.Concat(fData, "\"" + data.WorkZip + "\"", delim);
            fData = string.Concat(fData, "\"" + data.HomePhone + "\"", delim);
            fData = string.Concat(fData, "\"" + data.CellPhone + "\"", delim);
            fData = string.Concat(fData, "\"" + data.WorkPhone + "\"", delim);
            fData = string.Concat(fData, "\"" + data.AppEmailAddress + "\"", delim);
            fData = string.Concat(fData, "\"" + data.DOB + "\"", delim);
            fData = string.Concat(fData, "\"" + data.Company + "\"", delim);
            fData = string.Concat(fData, "\"" + data.CompanyOrderID + "\"", delim);
            fData = string.Concat(fData, "\"" + data.Tracer + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ParamedOrderedDate + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ParamedScheduledDate + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ParamedCompleteDate + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ParamedCancelledDate + "\"", delim);
            //fData = string.Concat(fData, "\"" + ((data.ParamedOrderedDate.ToString() == "0001/1/1") ? "" : data.ParamedOrderedDate.ToString()) + "\"", delim);
            //fData = string.Concat(fData, "\"" + ((data.ParamedScheduledDate.ToString() == "1/1/000") ? "" : data.ParamedScheduledDate.ToString()) + "\"", delim);
            //fData = string.Concat(fData, "\"" + ((data.ParamedCompleteDate.ToString() == "1/1/0001") ? "" : data.ParamedCompleteDate.ToString()) + "\"", delim);
            //fData = string.Concat(fData, "\"" + ((data.ParamedCancelledDate.ToString() == "1/1/0001") ? "" : data.ParamedCancelledDate.ToString()) + "\"", delim);
            fData = string.Concat(fData, "\"" + data.GeneralStatus + "\"", delim);
            fData = string.Concat(fData, "\"" + data.Notes + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ExamAddress1 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ExamAddress2 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ExamAddress3 + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ExamCity + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ExamState + "\"", delim);
            fData = string.Concat(fData, "\"" + data.ExamZip + "\"", delim);
            fData = string.Concat(fData, "\"" + data.OfficePhone + "\"", delim);
            fData = string.Concat(fData, "\"" + ConfigurationManager.AppSettings["ApplicantSelfSchedulingURL"] + data.CompanyOrderID + "\"", delim);
            fData = string.Concat(fData, "\"" + data.StatusCode.ToString() + "\"", delim);
            fData = string.Concat(fData, "\"" + data.StatusDesc + "\"", delim);
            fData = string.Concat(fData, "\"" + data.Call_Ctr + "\"", delim);
            fData = string.Concat(fData, "\"" + data.Record_Type + "\"");
            fData = string.Concat(fData, Environment.NewLine);
            return fData;
        }

        public string BuildDataHeaderRow()
        {
            // add quotes to all datafields to allow commas within data
            // to become standard text, rather than acting as delimiters
            string fData = String.Empty;

            fData = string.Concat(fData, "\"" + "Applicant Last Name" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Applicant First Name" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Home Address 1" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Home Address 2" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Home Address 3" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Home City" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Home State" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Home Zip" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Work Address 1" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Work Address 2" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Work Address 3" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Work City" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Work State" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Work Zip" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Home Phone" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Cell Phone" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Work Phone" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Applicant Email Address" + "\"", delim);
            fData = string.Concat(fData, "\"" + "DOB" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Company" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Order ID" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Tracer ID" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Paramed Ordered Date" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Paramed Scheduled Date" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Paramed Complete Date" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Paramed Cancelled Date" + "\"", delim);
            fData = string.Concat(fData, "\"" + "General Status" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Notes" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Exam Address 1" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Exam_Address_2" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "Exam_Address_3" + "\"", delim);
            fData = string.Concat(fData, "\"" + "Exam_City" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "Exam_State" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "Exam_Zip" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "Office Phone" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "URL" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "Status_Code" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "Status_Desc" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "Call_Ctr" + "\"", delim);
            fData = string.Concat(fData, "\"" +  "Report_Type" + "\"");        
            fData = string.Concat(fData, Environment.NewLine);
            return fData;
        }
    }
}
