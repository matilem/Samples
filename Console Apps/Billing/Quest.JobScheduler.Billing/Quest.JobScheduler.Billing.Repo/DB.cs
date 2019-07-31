using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using ExamCentral.Core.Logging;
using ExamCentral.Core.Interfaces;


namespace Quest.JobScheduler.Billing.Repo
{
    public class DB
    {        
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
                };

                conn.Dispose();
            }
        }

        private SqlParameter CreateParameter(string name, SqlDbType type, object value)
        {
            SqlParameter par = new SqlParameter(name, type);
            par.Value = value;

            return par;
        }

        private SqlParameter CreateParameter(string name, SqlDbType type, int length, object value)
        {
            SqlParameter par = new SqlParameter(name, type, length);
            par.Value = value;

            return par;
        }

        public int p_billingload(DateTime BillingDate)
        {
            int recordsProcessed = 0;

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Billing.DBConnection"].ConnectionString);

                using (var cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[Billing]";//need to update this

                    cmd.Parameters.Add(CreateParameter("@ProcessDate", SqlDbType.DateTime, BillingDate));

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        //Build XML Objects
                    }

                    recordsProcessed = 0;//need to update this

                    
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Database Exception: {0}, {1}", ex.Message, ex.StackTrace);
            }

            Dispose(conn);

            return recordsProcessed;
        }

        public int p_billingfile()
        {
            int recordsProcessed = 0;

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Billing.DBConnection"].ConnectionString);

                using (var cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[BillingHistory]";//need to update this

                    //cmd.Parameters.Add(CreateParameter("@ProcessDate", SqlDbType.DateTime, BillingDate));

                    recordsProcessed = 0;//need to update this
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Database Exception: {0}, {1}", ex.Message, ex.StackTrace);
            }

            Dispose(conn);
            
            return recordsProcessed;
        }

        public int p_billinghistory()
        {
            int recordsProcessed = 0;

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Billing.DBConnection"].ConnectionString);

                using (var cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[BillingHistory]";//need to update this

                    //cmd.Parameters.Add(CreateParameter("@ProcessDate", SqlDbType.DateTime, BillingDate));

                    recordsProcessed = 0;//need to update this
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Database Exception: {0}, {1}", ex.Message, ex.StackTrace);
            }

            Dispose(conn);

            return recordsProcessed;
        }

    }
}