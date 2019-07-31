using System;

namespace AdminTool.Api.Models
{
    public class RolePermission
    {
        public int RoleId { get; set; }

        public string sAMAccountName { get; set; }

        public bool ActiveFlag { get; set; }

        public DateTime AddDate { get; set; }

        public string AddUser { get; set; }

        public DateTime ChangeDate { get; set; }

        public string ChangeUser { get; set; }
    }
}