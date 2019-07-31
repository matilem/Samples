namespace AdminTool.ViewModels
{
    public class HydraUserPermissionViewModel
    {
        public int PermissionId { get; set; }

        public int ResourceId { get; set; }

        public string ApplicationName { get; set; }

        public string InterfaceId { get; set; }

        public string FunctionId { get; set; }

        public bool ActiveStatus { get; set; }
    }
}