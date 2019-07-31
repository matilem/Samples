namespace AdminTool.Api.Models
{
    public class Permission
    {
        public virtual int PermissionId { get; set; }

        public virtual string AccessName { get; set; }

        public virtual string AccessType { get; set; }

        public int ResourceId { get; set; }

        public bool RightPermission { get; set; }
    }
}