using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace TavocaFirstCallFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["StatusFolder"], "*.dat");

            string fileName = ConfigurationManager.AppSettings["ProcessedFolder"] + "\\" + DateTime.Now.ToString("MMddyyyy HHmmss").Replace(" ", "_") + "_error.log";

            FileStream fsOutput = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fsOutput);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EOConnString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("p_InsertFirstCallAPM", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@app_id", SqlDbType.Int);
            cmd.Parameters.Add("@call_date", SqlDbType.DateTime);
            cmd.Parameters.Add("@call_time", SqlDbType.DateTime);
            cmd.Parameters.Add("@SMS_code", SqlDbType.Int);

            foreach (string file in files)
            {
                string delimeter = "\t";
                FileStream fsInput = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fsInput);

                string line = String.Empty;
                string[] columns;
  
                while ((line = sr.ReadLine()) != null)
                {
                    columns = line.Split(delimeter.ToCharArray());
                    cmd.Parameters["@app_id"].Value = Int32.Parse(columns[0]);

                    if (columns[1].ToString() == String.Empty)
                    {
                        cmd.Parameters["@call_date"].Value = DBNull.Value;
                        cmd.Parameters["@call_time"].Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters["@call_date"].Value = Convert.ToDateTime(columns[1] + " 00:00");
                        cmd.Parameters["@call_time"].Value = Convert.ToDateTime("01/01/1900 " + columns[2]);
                    }

                    cmd.Parameters["@SMS_code"].Value = Int32.Parse(columns[3]);

                    try
                    {
                        int ret = (int)cmd.ExecuteScalar();

                        if (ret == 0)
                        {
                            sw.WriteLine("Cannot Insert for Applicant: " + columns[0] + " Call DateTime: " + columns[1] + " " + columns[2] +
                                " Status Code: " + columns[3]);
                        }
                    }
                    catch(Exception ex)
                    {
                        sw.WriteLine(ex.Message + ": " + line);
                    }
                    

                }

                sr.Close();
                fsInput.Close();

                Directory.Move(file, file.Replace(ConfigurationManager.AppSettings["StatusFolder"], ConfigurationManager.AppSettings["ProcessedFolder"]));

            }

            conn.Close();
            

            sw.Flush();
            long fileLength = fsOutput.Length;
            sw.Close();
            fsOutput.Close();

            if (fileLength != 0)
            {
                MailMessage eMsg = new MailMessage();
                Attachment mailAttach = new Attachment(fileName);

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

                //eMsg.CC.Add(new MailAddress(ConfigurationManager.AppSettings["emailCC"]));
                eMsg.Subject = "Error Inserting First Call APM Status for Applicants";
                eMsg.Body = "Please check the errors in the attachment enclosed to resolve issues. \n\n  Thank you.";
                eMsg.IsBodyHtml = false;
                eMsg.Attachments.Add(mailAttach);

                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings["mailServer"];
                client.UseDefaultCredentials = true;
                client.Send(eMsg);
            }
            else
            {
                File.Delete(fileName);
            }
      
        }
    }
}
