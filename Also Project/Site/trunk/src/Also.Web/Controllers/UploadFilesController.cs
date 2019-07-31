using Aafp.Also.Web.Filters;
using Aafp.Also.Web.Tasks.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Aafp.Also.Web.Controllers
{
    [RoutePrefix("fileupload")]
    public class UploadFilesController : Controller
    {
        public IFileTasks FileTasks { get; set; }

        // GET: UploadFiles
        [Route("upload")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("ajaxupload")]
        [HttpPost]
        public async Task<ActionResult> AjaxUploadAsync()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                var userId = GetUserName();
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

                    var also_guid = Request.Form.GetValues("also_courseKey").First();

                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);  // temporary upload destination will replace with valid file server

                        //// ok we don't want to save it here, this is the time to send file object to API
                        ////file.SaveAs(fname);
                        Guid alsoCourseKey = Guid.Parse(also_guid);
                        var result = await FileTasks.SaveFile(file, userId, alsoCourseKey);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        private string GetUserName()
        {
            return SiteMinderAuthenticationFilter.GetUserName(System.Web.HttpContext.Current.Request);
        }
    }
}