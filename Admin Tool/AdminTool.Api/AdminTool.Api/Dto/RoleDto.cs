namespace AdminTool.Api.Dto
{
    public class RoleDto
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string RoleGroup { get; set; }

        public bool ActiveFlag { get; set; }
    }
}