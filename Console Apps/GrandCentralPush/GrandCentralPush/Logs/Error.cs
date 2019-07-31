using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace GrandCentralPush.Logs
{
    class Error : LogBase
    {
        public Error(DateTime sDate, DateTime eDate)
        {
            StartDate = sDate;
            EndDate = eDate;
            FileName = String.Concat(ConfigurationManager.AppSettings["ErrorFilePath"], "Error", this.EndDate.ToString("MMddyyyyhhmmss"), ".txt");
        }

        public void WriteToFile()
        {
            WriteToFile(FileName);
        }
    }
}
