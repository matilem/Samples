using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GrandCentralPush.Logs
{
    class LogBase
    {
        protected MemoryStream MemoryStream;
        protected StreamWriter StreamWriter;
        public DateTime StartDate;
        public DateTime EndDate;
        public string FileName;

        public LogBase()
        {
            this.MemoryStream = new MemoryStream();
            this.StreamWriter = new StreamWriter(MemoryStream);
        }

        public void WriteLine(string msg)
        {
            StreamWriter.WriteLine(msg);
        }

        protected void WriteToFile(string fileName)
        {
            try
            {
                this.StreamWriter.Flush();
                this.MemoryStream.WriteTo(File.Create(fileName));
            }
            catch (Exception e)
            {
                throw new Exception("Error while trying to write to file", e);
            }
            finally
            {
                CloseStreams();
            }
        }

        private void CloseStreams()
        {
            StreamWriter.Close();
            MemoryStream.Close();
        }
    }
}
