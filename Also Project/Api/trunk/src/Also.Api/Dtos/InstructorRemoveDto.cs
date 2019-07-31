using System;

namespace Aafp.Also.Api.Dtos
{
    public class InstructorRemoveDto
    {
        public Guid InstructorKey { get; set; }

        public Guid CustomerKey { get; set; }
    }
}