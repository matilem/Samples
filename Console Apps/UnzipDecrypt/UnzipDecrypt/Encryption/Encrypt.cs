using System;
using System.Linq;
using System.Collections;
using System.IO;
using UnzipDecrypt.Utilities;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Security;

namespace UnzipDecrypt.Encrypt
{
    public class Encrypt
    {
        public static void EncryptFile(
        string outputFileName,
        string inputFileName,
        string encKeyFileName,
        bool armor,
        bool withIntegrityCheck,
        string defaultFilePath)
        {
            if (!defaultFilePath.EndsWith("\\"))
            {
                defaultFilePath += "\\";
            }

            PgpPublicKey encKey = PgpUtils.ReadPublicKey(encKeyFileName);

            using (Stream output = File.Create(outputFileName))
            {
                EncryptFile(output, inputFileName, encKey, armor, withIntegrityCheck, defaultFilePath);
            }
        }

        private static void EncryptFile(
            Stream outputStream,
            string fileName,
            PgpPublicKey encKey,
            bool armor,
            bool withIntegrityCheck,
            string defaultFilePath)
        {
            if (armor)
            {
                outputStream = new ArmoredOutputStream(outputStream);
            }

            try
            {
                byte[] bytes = PgpUtils.CompressFile(fileName, CompressionAlgorithmTag.Zip);

                PgpEncryptedDataGenerator encGen = new PgpEncryptedDataGenerator(
                    SymmetricKeyAlgorithmTag.Cast5, withIntegrityCheck, new SecureRandom());
                encGen.AddMethod(encKey);

                Stream cOut = encGen.Open(outputStream, bytes.Length);

                cOut.Write(bytes, 0, bytes.Length);
                cOut.Close();

                if (armor)
                {
                    outputStream.Close();
                }
            }
            catch (PgpException e)
            {
                Console.Error.WriteLine(e);

                Exception underlyingException = e.InnerException;
                if (underlyingException != null)
                {
                    Console.Error.WriteLine(underlyingException.Message);
                    Console.Error.WriteLine(underlyingException.StackTrace);
                }
            }
        }
    }
}
