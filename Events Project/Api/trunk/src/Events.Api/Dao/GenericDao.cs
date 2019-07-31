using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class GenericDao<T>
    {
        public ISession Session { get; set; }

        public IList<T> GetAll()
        {
            return Session.Query<T>().ToList();
        }

        public T GetByKey(Guid key)
        {
            return Session.Get<T>(key);
        }

        public void Store(T type)
        {
            Session.SaveOrUpdate(type);
        }

        public void Delete(T type)
        {
            Session.Delete(type);
        }
    }
}