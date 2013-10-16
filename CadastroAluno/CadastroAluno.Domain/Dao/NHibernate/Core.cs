using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Context;

namespace CadastroAluno.Domain.Dao.NHibernate
{
    class Core : IHttpModule
    {
        private static readonly ISessionFactory _sessionFactory;

        static Core()
        {
            _sessionFactory = CreateSessionFactory();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        public static ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        public void Dispose() { }

        private static void BeginRequest(object sender, EventArgs e)
        {
            ISession session = _sessionFactory.OpenSession();
            session.BeginTransaction();
            CurrentSessionContext.Bind(session);
        }

        private static void EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(_sessionFactory);

            if (session == null) return;

            try
            {
                session.Transaction.Commit();
            }
            catch (Exception ex)
            {
                if (session.IsConnected)
                    try
                    {
                        session.Transaction.Rollback();
                    }
                    catch (Exception ex2) { }

                throw ex;
            }
            finally
            {
                session.Close();
                session.Dispose();
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            FluentConfiguration configuration = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.FormatSql().ConnectionString(
               x => x.FromConnectionStringWithKey("database")))
            .ExposeConfiguration(
                c => c.SetProperty("current_session_context_class", "web")
                      .SetProperty("sql_exception_converter", typeof(SqlExceptionConverter).AssemblyQualifiedName)
                      .SetProperty("cache.provider_class", typeof(HashtableCacheProvider).AssemblyQualifiedName)
                      .SetProperty("adonet.batch_size", "20")
                      .SetProperty("cache.use_second_level_cache", "true")
                      .SetProperty("hibernate.use_reflection_optimizer", "true")
                      .SetProperty("hibernate.cache.use_query_cache", "true")
                      )
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Core>());

            try
            {
                return configuration.BuildSessionFactory();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
