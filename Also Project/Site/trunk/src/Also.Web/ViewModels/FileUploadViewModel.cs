using System.Web;

namespace Aafp.Also.Web.ViewModels
{
    public class FileUploadViewModel
    {
        public HttpPostedFileBase File { get; set; }
        public string OrgID { get; set; }
        public byte[] Data { get; set; }
    }
}