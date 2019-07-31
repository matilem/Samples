using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.MyCme.Web.ViewModels
{
    public class LmsAccessViewModel
    {
        public int CourseId { get; set; }

        public bool HasAccess { get; set; }

        public string SsoRedirect => $"";

        public bool HasLmsCommunicationError { get; set; }
    }
}