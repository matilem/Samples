using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GrandCentralPush.CSV
{
    class CSVFile
    {
        public bool Save(string fileData, string fileNameWithPath)
        {
            // save file here
            File.WriteAllText(fileNameWithPath, fileData);
            return true;
        }
    }
}
