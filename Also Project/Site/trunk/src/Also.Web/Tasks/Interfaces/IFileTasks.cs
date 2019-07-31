using Aafp.Also.Web.ViewModels;
using System.Threading.Tasks;
using System.Web;
using System;

namespace Aafp.Also.Web.Tasks.Interfaces
{
    public interface IFileTasks
    {
        Task<bool> SaveFile(HttpPostedFileBase file, string userId, Guid alsoKey);
    }
}