using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Cme.Api.Dtos.Lms
{
    public class LmsEnrollmentCreateDto
    {
        public int Nid { get; set; }

        public int Uid { get; set; }

        public string EnrollmentType => "webservice_call";

        public string Type => "course_enrollment";

        public int Status => 1;
    }
}