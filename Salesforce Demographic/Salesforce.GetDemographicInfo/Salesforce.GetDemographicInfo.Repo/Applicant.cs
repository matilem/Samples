using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salesforce.GetDemographicInfo.Repo
{
    public class Applicant
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Pol_Amt { get; set; }
        public int Global_App_SEQ_ID { get; set; }
        public DateTime App_Create_Dt { get; set; }
        public string Lab_Code { get; set; }
        public string Tracer { get; set; }
        public DateTime Birthdate { get; set; }
        public string Global_App_OFC_Code { get; set; }
        public Services Services { get; set; }
        public Insurance Insurance { get; set; }
    }

    public class Services
    {
        public Service[] Service { get; set; }
    }

    public class Service
    {
        public DateTime Completed_Date { get; set; }
        public string SVD_Code { get; set; }
        public string Description { get; set; }
        public Examiner Examiner { get; set; }
    }

    public class Examiner
    {
        public int OFC_EXA_ID { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public Office Office { get; set; }
    }

    public class Office
    {
        public string EXA_Code { get; set; }
        public string State { get; set; }
    }

    public class Insurance
    {
        public string INS_Code { get; set; }
        public string PHYS_Name { get; set; }
        public Agent Agent { get; set; }
    }

    public class Agent
    {
        public string AGT_Code { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email_Address { get; set; }
        public int Global_AGT_ID { get; set; }
        public int AGT_ID { get; set; }
    }
}
