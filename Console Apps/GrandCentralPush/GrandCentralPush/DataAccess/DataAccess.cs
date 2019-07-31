using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Configuration;

namespace GrandCentralPush.DataAccess
{
    class DataAccess
    {
        public List<Data.Data> ReadByDateRange(DateTime startDate, DateTime endDate)
        {
            Reader.Reader mlr = new Reader.Reader();
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("p_GrandCentral_Fetch", conn))
                {
                    cmd.CommandTimeout = 10000;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pStartDate = new SqlParameter("@startDate", SqlDbType.DateTime);
                    pStartDate.Value = startDate;
                    cmd.Parameters.Add(pStartDate);

                    SqlParameter pEndDate = new SqlParameter("@endDate", SqlDbType.DateTime);
                    pEndDate.Value = endDate;
                    cmd.Parameters.Add(pEndDate);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                mlr.Read(dr);
                            }
                        }
                    }
                }
            }
            return mlr.data;
        }
    }
}
