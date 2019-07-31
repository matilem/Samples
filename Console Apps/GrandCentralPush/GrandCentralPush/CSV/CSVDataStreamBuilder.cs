using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GrandCentralPush.CSV
{
    class CSVDataStreamBuilder
    {
        string XMIT = ConfigurationManager.AppSettings["XMIT"];

        public string BuildDataList(List<Data.Data> readData)
        {
            CSVRowBuilder row = new CSVRowBuilder();
            string DataList = XMIT;
            DataList = string.Concat(DataList, Environment.NewLine);

            DataList = string.Concat(DataList, row.BuildDataHeaderRow());
            Data.Data dd = null;
            int i = 1;
            try
            {
                foreach (Data.Data d in readData)
                {
                    i++;
                    dd = d;
                    DataList = string.Concat(DataList, row.BuildDataRow(d));
                }
            }
            catch
            {
                Console.WriteLine(dd.CompanyOrderID.ToString() + " -- " + i.ToString());
            }
            return DataList;
        }
    }
}
