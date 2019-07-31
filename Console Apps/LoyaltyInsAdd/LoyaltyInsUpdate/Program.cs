using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

namespace LoyaltyInsUpdate
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);
            //Console.WriteLine(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);

            try
            {
                sqlConn.Open();
               // Console.WriteLine("OpenConnection");

                SqlCommand cmd = new SqlCommand("p_LoyaltyInsAdd", sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
               // Console.WriteLine("Execute Query");

            }
            catch (Exception ex)
                
            {
                ProgramError.SendErrorMail(String.Format("Method: FetchRecords(). Exception: {0}", ex.Message));
            }
            finally
            {
                if (sqlConn != null)
                {
                    sqlConn.Close();
                    //Console.WriteLine("CloseConnection");
                }

            }
        }
    }

    class ProgramError
    {
        static public void SendErrorMail(string Message)
        {
            try
            {
                MailMessage eMsg = new MailMessage();

                string delimiter = ";";
                string line = string.Empty;
                string[] columns;

                eMsg.From = new MailAddress(ConfigurationManager.AppSettings["emailFrom"]);
                line = ConfigurationManager.AppSettings["emailTo"];
                //parse to address line from file using comma delimiter
                columns = line.Split(delimiter.ToCharArray());
                foreach (string address in columns)
                {
                    eMsg.To.Add(new MailAddress(address));
                }

                line = ConfigurationManager.AppSettings["emailCC"];
                //parse to address line from file using comma delimiter
                columns = line.Split(delimiter.ToCharArray());
                foreach (string address in columns)
                {
                    eMsg.CC.Add(new MailAddress(address));
                }

                eMsg.Subject = "Error During Loyalty Insurance Add";
                eMsg.Body = Message;
                eMsg.IsBodyHtml = false;

                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings["mailServer"];
                client.UseDefaultCredentials = true;
                client.Send(eMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
