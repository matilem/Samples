using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Models;
using System;

namespace Aafp.Cme.Api.Daos.Commands.Interfaces
{
    public interface ICreditCommand
    {
        Credit ReportCredit(Guid customerKey, CmeActivitySessionDto session, string webLogin);

        Credit ReportTeachingCredit(Guid customerKey, CmeActivitySessionDto session, string webLogin);
    }
}
