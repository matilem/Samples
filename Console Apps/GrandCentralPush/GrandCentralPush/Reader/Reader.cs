using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GrandCentralPush.Reader
{
    class Reader
    {
        public List<Data.Data> data = new List<Data.Data>();
        // Read data row and populate biz obj
        // return a list
        public void Read(SqlDataReader dr)
        {
            // add try catch
            Data.Data mlData = new GrandCentralPush.Data.Data();
            mlData.CompanyOrderID = GetDBInt32(dr, (int)Data.Data.Columns.CompanyOrderID);
            mlData.AppLastName = GetDBString(dr, (int)Data.Data.Columns.AppLastName);
            mlData.AppFirstName = GetDBString(dr, (int)Data.Data.Columns.AppFirstName);
            mlData.HomeAddress1 = GetDBString(dr, (int)Data.Data.Columns.HomeAddress1);
            mlData.HomeAddress2 = GetDBString(dr, (int)Data.Data.Columns.HomeAddress2);
            mlData.HomeAddress3 = GetDBString(dr, (int)Data.Data.Columns.HomeAddress3);
            mlData.HomeCity = GetDBString(dr, (int)Data.Data.Columns.HomeCity);
            mlData.HomeState = GetDBString(dr, (int)Data.Data.Columns.HomeState);
            mlData.HomeZip = GetDBString(dr, (int)Data.Data.Columns.HomeZip);
            mlData.WorkAddress1 = GetDBString(dr, (int)Data.Data.Columns.WorkAddress1);
            mlData.WorkAddress2 = GetDBString(dr, (int)Data.Data.Columns.WorkAddress2);
            mlData.WorkAddress3 = GetDBString(dr, (int)Data.Data.Columns.WorkAddress3);
            mlData.WorkCity = GetDBString(dr, (int)Data.Data.Columns.WorkCity);
            mlData.WorkState = GetDBString(dr, (int)Data.Data.Columns.WorkState);
            mlData.WorkZip = GetDBString(dr, (int)Data.Data.Columns.WorkZip);
            mlData.HomePhone = GetDBString(dr, (int)Data.Data.Columns.HomePhone);
            mlData.HomePhone = GetDBString(dr, (int)Data.Data.Columns.CellPhone);
            mlData.WorkPhone = GetDBString(dr, (int)Data.Data.Columns.WorkPhone);
            mlData.AppEmailAddress = GetDBString(dr, (int)Data.Data.Columns.AppEmailAddress);
            mlData.DOB = GetDBString(dr, (int)Data.Data.Columns.DOB);
            mlData.Company = GetDBString(dr, (int)Data.Data.Columns.Company);
            mlData.Tracer = GetDBString(dr, (int)Data.Data.Columns.Tracer);
            mlData.ParamedOrderedDate = GetDBString(dr, (int)Data.Data.Columns.ParamedOrderedDate);
            mlData.ParamedCompleteDate = GetDBString(dr, (int)Data.Data.Columns.ParamedCompleteDate);
            mlData.ParamedCancelledDate = GetDBString(dr, (int)Data.Data.Columns.ParamedCancelledDate);
            mlData.GeneralStatus = GetDBString(dr, (int)Data.Data.Columns.GeneralStatus);
            mlData.Notes = GetDBString(dr, (int)Data.Data.Columns.Notes);
            mlData.ParamedScheduledDate = GetDBString(dr, (int)Data.Data.Columns.ParamedScheduledDate);
            //mlData.AppointmentTime = GetDBString(dr, (int)Data.Data.Columns.AppointmentTime);
            //if (!mlData.AppointmentDate.Equals(string.Empty) && !mlData.AppointmentTime.Equals(string.Empty))
            //    mlData.ParamedScheduledDate = DateTime.Parse(mlData.AppointmentDate + " " + mlData.AppointmentTime);
            mlData.ExamAddress1 = GetDBString(dr, (int)Data.Data.Columns.ExamAddress1);
            mlData.ExamAddress2 = GetDBString(dr, (int)Data.Data.Columns.ExamAddress2);
            mlData.ExamAddress3 = GetDBString(dr, (int)Data.Data.Columns.ExamAddress3);
            mlData.ExamCity = GetDBString(dr, (int)Data.Data.Columns.ExamCity);
            mlData.ExamState = GetDBString(dr, (int)Data.Data.Columns.ExamState);
            mlData.ExamZip = GetDBString(dr, (int)Data.Data.Columns.ExamZip);
            mlData.OfficePhone = GetDBString(dr, (int)Data.Data.Columns.OfficePhone);
            mlData.StatusCode = GetDBString(dr, (int)Data.Data.Columns.StatusCode);
            mlData.StatusDesc = GetDBString(dr, (int)Data.Data.Columns.StatusDesc);
            mlData.Call_Ctr = GetDBString(dr, (int)Data.Data.Columns.Call_Ctr);
            mlData.Record_Type = GetDBString(dr, (int)Data.Data.Columns.Record_Type);
            data.Add(mlData);
        }

        private string GetDBString(SqlDataReader dr, int column)
        {
            return dr.IsDBNull(column) ? String.Empty : dr.GetString(column);
        }

        private DateTime GetDBDateTime(SqlDataReader dr, int column)
        {
            return dr.IsDBNull(column) ? DateTime.MinValue : dr.GetDateTime(column);
        }

        private int GetDBInt32(SqlDataReader dr, int column)
        {
            return dr.IsDBNull(column) ? Int32.MinValue : dr.GetInt32(column);
        }
    }
}
