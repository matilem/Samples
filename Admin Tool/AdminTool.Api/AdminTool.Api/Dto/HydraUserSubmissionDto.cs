using System.Collections.Generic;

namespace AdminTool.Api.Dto
{
    public class HydraUserSubmissionDto
    {
        public string UserName { get; set; }

        //public IList<HydraUserPermissionDto> Permissions { get; set; }

        public string Status { get; set; }
    }
}