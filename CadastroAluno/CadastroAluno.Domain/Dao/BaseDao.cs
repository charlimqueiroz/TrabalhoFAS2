using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CadastroAluno.Domain.Dao.NHibernate;
using NHibernate;

namespace CadastroAluno.Domain.Dao
{
    internal class BaseDao
    {
        /// <summary>
        /// NHibernate session.
        /// </summary>
        protected static ISession nhibernateSession
        {

            get { return Core.GetCurrentSession(); }

        }
    }
}
