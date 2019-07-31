namespace AdminTool.Api.Models
{
    public class SecuredResource
    {
        public virtual int ResourceId { get; set; }

        public virtual string ApplicationName { get; set; }

        public virtual string InterfaceId { get; set; }

        public virtual string FunctionId { get; set; }

        public virtual string FeatureId { get; set; }

        public virtual bool ActiveStatus { get; set; } 
    }
}