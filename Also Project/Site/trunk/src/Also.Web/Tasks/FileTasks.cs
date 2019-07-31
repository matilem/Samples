using Aafp.Also.Web.Tasks.Interfaces;
using Aafp.Also.Web.ViewModels;
using Aafp.Also.Web.Models;
using ApiClientHelper.Components;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System;

namespace Aafp.Also.Web.Tasks
{
    public class FileTasks : IFileTasks
    {
        public async Task<bool> SaveFile(HttpPostedFileBase file, string userId, Guid alsoKey)
        {
            FileUpload upload = new FileUpload
            {
                Attachment = new Attachment(),
                OrgID = userId,
                Data = ReadToEnd(file.InputStream),
                CourseKey = alsoKey
            };

            upload.Attachment.FileLocation = file.FileName;
            upload.Attachment.CourseKey = alsoKey;
            var success = false;
            var result = await HttpClientHelper.PostJson<bool>(ApplicationConfig.AlsoServiceUrl, "file/save", upload);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }
            else
            {
                throw new ServiceException(result.ErrorMessage);
            }

            return success;
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}