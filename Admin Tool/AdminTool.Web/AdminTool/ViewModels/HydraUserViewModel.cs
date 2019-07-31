using System.Collections.Generic;

namespace AdminTool.ViewModels
{
    public class HydraUserViewModel
    {
        public string UserName { get; set; }

        public IList<HydraUserPermissionViewModel> Permissions { get; set; }

        public string Status { get; set; }
    }
}