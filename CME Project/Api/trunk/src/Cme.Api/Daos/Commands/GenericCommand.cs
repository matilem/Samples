using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using System;

namespace Aafp.Cme.Api.Daos.Commands
{
    public class GenericCommand<T>
    {
        public ISession Session { get; set; }

        public IList<T> GetAll()
        {
            return Session.Query<T>().ToList();
        }

        public T GetById(int id)
        {
            return Session.Get<T>(id);
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

        public ISession GetOpenSession()
        {
            if (Session.IsOpen)
                return Session;

            return Session.SessionFactory.OpenSession();
        }

        public void CloseSession()
        {
            Session.Close();
            Session.Dispose();
        }
    }
}