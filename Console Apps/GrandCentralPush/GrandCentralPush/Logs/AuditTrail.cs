using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;

namespace GrandCentralPush.Logs
{
    class AuditTrail : LogBase
    {
        public AuditTrail(DateTime sDate, DateTime eDate)
        {
            this.StartDate = sDate;
            this.EndDate = eDate;
            this.FileName = String.Concat(ConfigurationManager.AppSettings["AuditFilePath"], "Audit", this.EndDate.ToString("MMddyyyyhhmmss"), ".txt");
        }

        public void WriteToFile()
        {
            base.WriteToFile(this.FileName);
        }
    }
}