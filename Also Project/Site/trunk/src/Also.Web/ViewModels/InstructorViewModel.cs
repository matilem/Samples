﻿using System;

namespace Aafp.Also.Web.ViewModels
{
    public class InstructorViewModel
    {
        public Guid InstructorKey { get; set; }

        public Guid CustomerKey { get; set; }

        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CurrentAlsoStatus { get; set; }

        public bool AdvisoryFacultyRecommended { get; set; }

        public bool InstructorRecommended { get; set; }
    }
}