using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace TractionCalc.Models
{
    public class DaneRepository
    {
        private ISession session;

        public DaneRepository(ISession session)
        {
            this.session = session;
        }

        public void SaveData(Dane dane)
        {
            try
            {
                session.Transaction.Begin();
                session.Save(dane);
                session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
            }
        }

        public void UpdateData(Dane dane)
        {
            try
            {
                session.Transaction.Begin();
                session.Update(dane);
                session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
            }
        }

        public void DeleteData(int id)
        {
            var dane = session.Get<Dane>(id);
            try
            {
                session.Transaction.Begin();
                session.Delete(dane);
                session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
            }
        }

        public List<Dane> DaneList()
        {
            var list = session.Query<Dane>().ToList();
            return list;
        }

        public Dane GetDaneRecord(int id)
        {
            return session.Get<Dane>(id);
        }
    }
}
