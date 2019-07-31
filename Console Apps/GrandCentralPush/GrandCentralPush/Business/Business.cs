using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace GrandCentralPush.Business
{
    class Business
    {
        public DateTime StartDate;
        public DateTime EndDate;

        public Business()
        {
            this.StartDate = GetStartDate();
            this.EndDate = GetEndDate();
        }

        public List<Data.Data> ReadByDateRange()
        {
            DataAccess.DataAccess mlDA = new DataAccess.DataAccess();
            return mlDA.ReadByDateRange(StartDate, EndDate);
        }

        private DateTime GetStartDate()
        {
            DateTime startDate;

            // for testing - set date to minValue
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    startDate = new DateTime(1900, 1, 1);
            //    //FileStream fs = null;
            //    //string lastRunFile = string.Concat(ConfigurationManager.AppSettings["LastRunFilePath"], "LastRun.txt");
            //    //fs = new FileStream(lastRunFile, FileMode.Open, FileAccess.Read);
            //    //BinaryFormatter bf = new BinaryFormatter();
            //    //startDate = (DateTime)bf.Deserialize(fs);
            //    //startDate = startDate.AddMilliseconds(1);
            //}
            //else
            //{
                FileStream fs = null;
                try
                {
                    string lastRunFile = string.Concat(ConfigurationManager.AppSettings["LastRunFilePath"], "LastRun.dat");
                    fs = new FileStream(lastRunFile, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    object dt = bf.Deserialize(fs);
                    startDate = System.DateTime.Parse(dt.ToString());
                    startDate = startDate.AddMilliseconds(1);
                }
                catch (FileNotFoundException)
                {
                    return new DateTime(1900, 1, 1);
                }
                finally
                {
                    fs.Close();
                }
            //}
            return startDate;
        }

        private DateTime GetEndDate()
        {
            DateTime now = DateTime.Now;
            now = now.AddMinutes(now.Minute * -1);
            now = now.AddSeconds(now.Second * -1);
            now = now.AddMilliseconds(now.Millisecond * -1);

            now = now.AddMilliseconds(-1);

            return now;
        }

    }
}
