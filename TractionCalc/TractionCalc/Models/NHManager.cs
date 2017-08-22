using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace TractionCalc.Models
{
    class NHManager : IDisposable
    {
        private static ISession Session;
        public static ISession OpenSession()
        {
            string connectionString = @"DataSource=Server=K14-BARTEK\\DGO;InitialCatalog=TractionCalc;IntegratedSecurity=True";
            string cns = "Server=K14-BARTEK\\DGO;Database=TractionCalc;Trusted_Connection=True";
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                  .ConnectionString(cns).ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Dane>())
                              .ExposeConfiguration(cfg => new SchemaExport(cfg)
                              .Create(false, false))
                            .BuildSessionFactory();
            Session = sessionFactory.OpenSession();
            return Session;
        }
        
        public void Dispose()
        {
            if(Session.IsOpen)
                Session.Dispose();

            this.Dispose();
        }
    }
}
