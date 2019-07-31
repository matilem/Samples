using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;


namespace UnzipDecrypt
{
    class Program
    {
        public static void Main(
           string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("usage: KeyBasedFileProcessor -e|-d [-a|ai] file [secretKeyFile passPhrase|pubKeyFile]");
                return;
            }

            if (args[0].Equals("-e"))
            {
                if (args[1].Equals("-a") || args[1].Equals("-ai") || args[1].Equals("-ia"))
                {
                    Encrypt.Encrypt.EncryptFile(args[2] + ".asc", args[2], args[3], true, (args[1].IndexOf('i') > 0),args[4]);
                }
                else if (args[1].Equals("-i"))
                {
                    Encrypt.Encrypt.EncryptFile(args[2] + ".bpg", args[2], args[3], false, true, args[4]);
                }
                else
                {
                    Encrypt.Encrypt.EncryptFile(args[1] + ".bpg", args[1], args[2], false, false, args[4]);
                }
            }
            else if (args[0].Equals("-d"))
            {

                //Decrypt

                string inputfilename = ConfigurationManager.AppSettings["EncryptFilePath"];
                string keyfilename = ConfigurationManager.AppSettings["KeyFilePath"];
                string password = ConfigurationManager.AppSettings["Password"];
                string defaultfilename = string.Concat(ConfigurationManager.AppSettings["FilePath"] + "TEST_EXM1_ALLORDERS_METM_", System.DateTime.Now ,".csv");
                string defaultfilepath = ConfigurationManager.AppSettings["ZipFilePath"];

                Decrypt.Decrypt.DecryptFile(inputfilename,keyfilename,password.ToCharArray(),defaultfilename,defaultfilepath);

                //Unzip

                string zipPathAndFile = ConfigurationManager.AppSettings["ZipFilePath"] + "\\output.zip";
                string outputFolder = ConfigurationManager.AppSettings["CSVFilePath"];

                ZipFile.UnZip.UnZipFile(zipPathAndFile,outputFolder,false);

            }
            else
            {
                Console.Error.WriteLine("usage: KeyBasedFileProcessor -d|-e [-a|ai] file [secretKeyFile passPhrase|pubKeyFile]");
            }
        }
    }
}
;