using System.Collections.Generic;
using System.Threading.Tasks;
using Aafp.MyCme.Web.Dtos;
using Aafp.MyCme.Web.ViewModels;

namespace Aafp.MyCme.Web.Tasks.Interfaces
{
    public interface ICreditTasks
    {
        Task<JsonResultViewModel<List<CreditDto>>> SubmitCredits(string[] sessionKeys, string webLogin);
    }
}