using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aafp.MyCme.Web.Tasks.Interfaces;
using Aafp.MyCme.Web.ViewModels;
using ApiClientHelper.Components;

namespace Aafp.MyCme.Web.Tasks
{
    public class CmeActivityTasks : ICmeActivityTasks
    {
        public async Task<CmeActivityViewModel> GetCmeActivies(string activityNumber, string webLogin)
        {
            var viewModel = new CmeActivityViewModel();

            var result = await HttpClientHelper.GetJson<CmeActivityViewModel>(ApplicationConfig.CmeServiceUrl, $"activity/{activityNumber}/{webLogin}/");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                viewModel.HasError = true;
            }
            else
            {
                viewModel = result.Data;
                viewModel.ActivityCity = viewModel.Sessions[0].SessionCity;
                viewModel.ActivityState = viewModel.Sessions[0].SessionState;

                viewModel.SessionsByDate = GetCmeSessionByActivityDate(viewModel);
            }

            return viewModel;
        }

        private List<SessionsByDateViewModel> GetCmeSessionByActivityDate(CmeActivityViewModel viewModel)
        {
            var orderedSessions = viewModel.Sessions.OrderBy(x => x.SessionBeginDate).ToList();
            viewModel.Sessions = orderedSessions;
            var sessions = new List<SessionsByDateViewModel>();

            foreach (var date in viewModel.ActivityDates)
            {
                var sessionByDate = new SessionsByDateViewModel
                {
                    ActivityDate = date,
                    CmeSessions = new List<CmeActivitySessionViewModel>()
                };

                foreach (var session in viewModel.Sessions.Where(session => session.SessionBeginDate.Date == date.Date))
                {
                    sessionByDate.CmeSessions.Add(session);
                }

                sessions.Add(sessionByDate);
            }

            return sessions;
        }
    }
}