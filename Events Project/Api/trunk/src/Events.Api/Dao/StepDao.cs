using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Models;
using AutoMapper.QueryableExtensions;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class StepDao : GenericDao<Step>, IStepDao
    {
        public List<UserRegistrationNavigationStepDto> GetNavigationSteps(Guid eventKey)
        {
            return Session.Query<Step>().Where(x => x.EventKey == eventKey).Project().To<UserRegistrationNavigationStepDto>().ToList();
        } 
    }
}