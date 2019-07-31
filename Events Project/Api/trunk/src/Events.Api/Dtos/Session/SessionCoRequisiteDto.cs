using System;
using Aafp.Events.Api.Dao;

namespace Aafp.Events.Api.Dtos.Session
{
    public class SessionCoRequisiteDto
    {
        public Guid Key { get; set; }

        public Guid SessionKey { get; set; }

        public SessionDao Sessions { get; set; }
    }
}