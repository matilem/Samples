using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandCentralPush.Data
{
    class Data
    {
        // This models the query results, record_type must be the last record in the list.
        public enum Columns
        {
            CompanyOrderID = 0,
            AppLastName,
            AppFirstName,
            HomeAddress1,
            HomeAddress2,
            HomeAddress3,
            HomeCity,
            HomeState,
            HomeZip,
            WorkAddress1,
            WorkAddress2,
            WorkAddress3,
            WorkCity,
            WorkState,
            WorkZip,
            HomePhone,
            CellPhone,
            WorkPhone,
            AppEmailAddress,
            DOB,
            Company,
            Tracer,
            ParamedOrderedDate,
            ParamedScheduledDate,
            //AppointmentTime,
            ParamedCompleteDate,
            ParamedCancelledDate,
            GeneralStatus,
            Notes,
            ExamAddress1,
            ExamAddress2,
            ExamAddress3,
            ExamCity,
            ExamState,
            ExamZip,
            OfficePhone,
            StatusCode,
            StatusDesc,
            Call_Ctr, 
            Record_Type
        };

        public int CompanyOrderID;
        public string AppLastName;
        public string AppFirstName;
        public string HomeAddress1;
        public string HomeAddress2;
        public string HomeAddress3;
        public string HomeCity;
        public string HomeState;
        public string HomeZip;
        public string WorkAddress1;
        public string WorkAddress2;
        public string WorkAddress3;
        public string WorkCity;
        public string WorkState;
        public string WorkZip;
        public string HomePhone;
        public string CellPhone;
        public string WorkPhone;
        public string AppEmailAddress;
        public string DOB;
        public string Company;
        public string Tracer;
        public string ParamedOrderedDate;
        public string ParamedScheduledDate;
        public string ParamedCompleteDate;
        public string ParamedCancelledDate;
        public string GeneralStatus;
        public string Notes;
        //public string AppointmentDate;
        //public string AppointmentTime;
        public string ExamAddress1;
        public string ExamAddress2;
        public string ExamAddress3;
        public string ExamCity;
        public string ExamState;
        public string ExamZip;
        public string OfficePhone;
        public string StatusCode;
        public string StatusDesc;
        public string Call_Ctr;
        public string Record_Type;
    }    
}
