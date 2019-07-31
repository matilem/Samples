using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GrandCentralPush.Logs
{
    class LastRun : LogBase
    {
        private FileStream fs = null;

        public LastRun(DateTime sDate, DateTime eDate)
        {
            this.StartDate = sDate;
            this.EndDate = eDate;
            this.FileName = String.Concat(ConfigurationManager.AppSettings["LastRunFilePath"], "LastRun.dat");
        }
        public void Update()
        {
            try
            {

                fs = new FileStream(this.FileName, FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, this.EndDate.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Error while trying to write to LastRunFile", e);
            }
            finally
            {
                fs.Dispose();
            }
        }
    }
}
