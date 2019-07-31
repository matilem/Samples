using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Also.Web.Models
{
    public class FileUpload
    {
        public Attachment Attachment { get; set; }
        public string OrgID { get; set; }
        public byte[] Data { get; set; }
        public Guid CourseKey { get; set; }
    }
}