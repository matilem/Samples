using System.Collections.Generic;


namespace AdminTool.Api.Dto
{
    public class ActiveDirectoryUserDto
    {
        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string GivenName { get; set; }

        public List<ActiveDirectoryGroupDto> ActiveDirectoryGroups{ get; set;}
    }
}