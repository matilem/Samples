using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aafp.MyCme.Web.ViewModels;

namespace Aafp.MyCme.Web.Tasks.Interfaces
{
    public interface ILmsTasks
    {
        Task<LmsAccessViewModel> AccessCourse(string webLogin, string courseId);
    }
}
