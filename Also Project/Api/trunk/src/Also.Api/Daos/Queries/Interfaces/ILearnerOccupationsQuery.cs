﻿using Aafp.Also.Api.Dtos;
using System.Collections.Generic;

namespace Aafp.Also.Api.Daos.Queries.Interfaces
{
    public interface ILearnerOccupationsQuery
    {
        List<LearnerOccupationDto> GetLearnerOccupations();
    }
}