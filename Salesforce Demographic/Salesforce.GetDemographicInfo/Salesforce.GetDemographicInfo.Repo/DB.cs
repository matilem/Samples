using System;
using System.Data;
using System.Data.SqlClient;
using QuestDiagnostics.ExamCentral.Core.Interfaces;
using QuestDiagnostics.Paramed.Applicint.Core.Common;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Salesforce.GetDemographicInfo.Repo
{
    public class DB
    {
        private ILogger Logger { get; set; }

        public DB(ILogger Logger)
        {
            this.Logger = Logger;
        }

        public void Dispose()
        {
        }

        private void Dispose(SqlConnection conn)
        {
            if (conn != null)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
        }

        private SqlParameter CreateParameter(string name, SqlDbType type, object value)
        {
            SqlParameter par = new SqlParameter(name, type);
            par.Value = value;

            return par;
        }

        public Applicant p_GetDemographicInfo(int Id)
        {
            //Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "Begin DB.p_GetDemographicInfo."));

            Applicant applicant = new Applicant();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(ConfigurationManager.AppSettings["DatabaseConnection"]);

                using (var cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "p_GetDemographicInfo";

                    //Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "Executing DB.p_GetDemographicInfo."));

                    cmd.Parameters.Add(CreateParameter("@App_Id", SqlDbType.Int, Id));

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {

                        Agent agent = new Agent
                        {
                            AGT_Code = dr.GetString(11),
                            First_Name = dr.GetString(12),
                            Last_Name = dr.GetString(13),
                            Email_Address = dr.GetString(14),
                            Global_AGT_ID = dr.GetInt32(15),
                            AGT_ID = dr.GetInt32(16)
                        };

                        Insurance insurance = new Insurance
                        {
                            INS_Code = dr.GetString(9),
                            PHYS_Name = dr.GetString(10),
                            Agent = agent
                        };

                        applicant = new Applicant
                        {
                            First_Name = dr.GetString(0),
                            Last_Name = dr.GetString(1),
                            Pol_Amt = dr.GetDecimal(2).ToString(),
                            Global_App_SEQ_ID = dr.GetInt32(3),
                            App_Create_Dt = dr.GetDateTime(4),
                            Lab_Code = dr.GetString(5),
                            Tracer = dr.GetString(6),
                            Birthdate = dr.GetDateTime(7),
                            Global_App_OFC_Code = dr.GetString(8),
                            Insurance = insurance
                        };

                        dr.Close();
                        dr.Dispose();                        

                        //Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "End DB.p_GetDemographicInfo."));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Error, "p_GetDemographicInfo Error: " + ex.StackTrace + ex.Message));
            }

            Dispose(conn);
            return applicant;
        }

        public Services p_GetDemographicInfoSvc(int Id)
        {
            //Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "Begin DB.p_GetDemographicInfoSvc."));

            Services services = new Services();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(ConfigurationManager.AppSettings["DatabaseConnection"]);

                using (var cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "p_GetDemographicInfoSvc";

                    //Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "Executing DB.p_GetDemographicInfoSvc."));

                    cmd.Parameters.Add(CreateParameter("@App_Id", SqlDbType.Int, Id));

                    SqlDataReader dr = cmd.ExecuteReader();

                    IList<Service> servicelist = new List<Service>();

                    while (dr.Read())
                    {
                        Office office = new Office
                        {
                            EXA_Code = dr.GetString(0),
                            State = dr.GetString(1)
                        };

                        Examiner examiner = new Examiner
                        {
                            OFC_EXA_ID = dr.GetInt32(2),
                            Last_Name = dr.GetString(3),
                            First_Name = dr.GetString(4),
                            Office = office
                        };

                        Service service = new Service
                        {
                            Completed_Date = dr.GetDateTime(5),
                            SVD_Code = dr.GetString(6),
                            Description = dr.GetString(7),
                            Examiner = examiner
                        };

                        servicelist.Add(service);
                    }

                    dr.Close();
                    dr.Dispose();

                    services.Service = servicelist.ToArray();

                    //Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Trace, "End DB.p_GetDemographicInfoSvc."));
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Statics.BuildLogMessage(LogMessage.Level.Error, "p_GetDemographicInfoSvc Error: " + ex.StackTrace + ex.Message));
            }

            Dispose(conn);
    
            return services;
        }
    }
}
