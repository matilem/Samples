using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aafp.MyCme.Web.Tasks.Interfaces;

namespace Aafp.MyCme.Web.Controllers
{
    [RoutePrefix("lms")]
    public class LmsController : Controller
    {
        public ILmsTasks LmsTasks { get; set; }

        //[Route("access/{courseId}")]
        //public async Task<ActionResult> AccessCourse(string courseId)
        //{
            
        //}

        //[Route("transfer-cart")]
        //public async Task<ActionResult> TransferCart()
        //{
            
        //}
    }
}