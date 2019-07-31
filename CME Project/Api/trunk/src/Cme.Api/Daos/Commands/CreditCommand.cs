using Aafp.Cme.Api.Daos.Commands.Interfaces;
using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Models;
using System;

namespace Aafp.Cme.Api.Daos.Commands
{
    public class CreditCommand : GenericCommand<Credit>, ICreditCommand
    {
        public Credit ReportCredit(Guid customerKey, CmeActivitySessionDto session, string webLogin)
        {
            var credit = new Credit
            {
                CustomerKey = customerKey,
                PrescribedCredits = session.SessionPrescribedCredits,
                ElectiveCredits = session.SessionElectiveCredits,
                SessionKey = session.SessionKey,
                SessionTitle = session.SessionTitle,
                SessionCity = session.SessionCity,
                SessionStateCode = session.SessionState,
                ParticipationBeginDate = session.SessionBeginDate.Date,
                ParticipationEndDate = session.SessionEndDate.Date,
                CreditSourceKey = new Guid("8E31EADA-B0DD-4C7F-9CD0-B26D6299A2D6"),
                AddUser = webLogin,
                AddDate = DateTime.Now,
            };

            Store(credit);

            return credit;
        }

        public Credit ReportTeachingCredit(Guid customerKey, CmeActivitySessionDto session, string webLogin)
        {
            var credit = new Credit
            {
                CustomerKey = customerKey,
                PrescribedCredits = session.SessionPrescribedCredits,
                ElectiveCredits = session.SessionElectiveCredits,
                SessionKey = session.SessionKey,
                SessionTitle = session.SessionTitle,
                SessionCity = session.SessionCity,
                SessionStateCode = session.SessionState,
                ParticipationBeginDate = session.SessionBeginDate.Date,
                ParticipationEndDate = session.SessionEndDate.Date,
                CreditSourceKey = new Guid("8E31EADA-B0DD-4C7F-9CD0-B26D6299A2D6"),
                CreditTypeKey = new Guid("A3F70592-837E-4918-B643-0ED4D673858E"),
                AddUser = webLogin,
                AddDate = DateTime.Now,
            };

            Store(credit);

            return credit;
        }

        public new void Store(Credit credit)
        {
            base.Store(credit);
            Session.Flush();
        }
    }
}