using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.ViewModels
{
    public class UploadFileResultModel
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string DeleteUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        public string DeleteType { get; set; }

        public string AttachmentKey { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}