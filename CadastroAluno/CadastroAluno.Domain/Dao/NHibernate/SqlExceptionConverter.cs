using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using NHibernate.Exceptions;

namespace CadastroAluno.Domain.Dao.NHibernate
{
    public class SqlExceptionConverter : ISQLExceptionConverter
    {
        public Exception Convert(AdoExceptionContextInfo exInfo)
        {
            var exception = ADOExceptionHelper.ExtractDbException(exInfo.SqlException) as MySqlException;
            return SQLStateConverter.HandledNonSpecificException(exInfo.SqlException, exInfo.Message, exInfo.Sql);
        }
    }
}
