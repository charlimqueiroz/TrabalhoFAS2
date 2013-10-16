using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Exceptions;

namespace CadastroAluno.Domain.Dao
{
    internal class CommonDao : BaseDao
    {
        private static ITransaction _transaction;

        /// <summary>
        /// Get the specified entity by ID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">Identifier of an entity.</param>
        /// <returns>Unique entity whether found and null whether not.</returns>
        public static T BuscarPorCodigo<T>(long id)
        {
            return nhibernateSession.Get<T>(id);
        }

        /// <summary>
        /// Get specified entity by the specified property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="description"></param>
        /// <returns>Unique entity whether found and null whether not.</returns>
        public static T BuscarPorPropriedade<T>(string propertyName, object description)
        {
            ICriteria crit = nhibernateSession.CreateCriteria(typeof(T));
            crit.Add(Expression.Eq(propertyName, description));
            object result = crit.UniqueResult();

            if (result != null)
                return (T)result;

            return default(T);
        }

        public static IList<T> ListarPorPropriedade<T>(string propertyName, object description, string[] sortProperties)
        {
            ICriteria crit = nhibernateSession.CreateCriteria(typeof(T));

            if (description != null)
                if (description is String)
                {
                    var d = description.ToString();

                    if (!string.IsNullOrEmpty(d))
                        crit.Add(Expression.Like(propertyName, d, MatchMode.Anywhere));
                }
                else
                    crit.Add(Expression.Eq(propertyName, description));

            if (sortProperties != null && sortProperties.Length > 0)
                foreach (string item in sortProperties)
                    crit.AddOrder(Order.Asc(item));

            return crit.List<T>();
        }
        public static IList<T> ListarPorPropriedade<T>(string propertyName, object description)
        {
            ICriteria crit = nhibernateSession.CreateCriteria(typeof(T));

            if (description != null)
                if (description is String)
                {
                    var d = description.ToString();

                    if (!string.IsNullOrEmpty(d))
                        crit.Add(Expression.Like(propertyName, d, MatchMode.Anywhere));
                }
                else
                    crit.Add(Expression.Eq(propertyName, description));

            return crit.List<T>();
        }

        public static IList BuscarTodos(Type type)
        {
            return BuscarTodos(type, null);
        }

        public static IList BuscarTodos(Type type, params string[] sortProperties)
        {
            ICriteria crit = nhibernateSession.CreateCriteria(type);

            if (sortProperties != null)
            {
                foreach (string sortProperty in sortProperties)
                {
                    crit.AddOrder(Order.Asc(sortProperty));
                }
            }

            return crit.List();
        }

        /// <summary>
        /// Get all objects of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> BuscarTodos<T>()
        {
            return BuscarTodos<T>(null);
        }

        /// <summary>
        /// Get all objects of T.
        /// </summary>
        /// <param name="sortProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> BuscarTodos<T>(params string[] sortProperties)
        {
            ICriteria crit = nhibernateSession.CreateCriteria(typeof(T));

            if (sortProperties != null)
            {
                foreach (string sortProperty in sortProperties)
                {
                    crit.AddOrder(Order.Asc(sortProperty));
                }
            }
            return crit.List<T>();
        }

        /// <summary>
        /// Get all objects of T for the given id's.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static IList<T> BuscarPorIds<T>(long[] ids)
        {
            ICriteria crit = nhibernateSession.CreateCriteria(typeof(T))
                .Add(Expression.In("id", ids));

            return crit.List<T>();
        }

        public static bool Exists<T>(Dictionary<string, object> parameters)
        {
            ICriteria crit = nhibernateSession.CreateCriteria(typeof(T));

            foreach (var parameter in parameters)
            {
                crit.Add(Expression.Eq(parameter.Key, parameter.Value));
            }

            crit.SetProjection(Projections.Count("id"));

            return Convert.ToInt32(crit.UniqueResult()) > 0;
        }

        public static void Salvar(object obj, bool useTransaction = true)
        {
            try
            {
                nhibernateSession.SaveOrUpdate(obj);
            }
            catch (ConstraintViolationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Atualizar(object obj, bool useTransaction = true)
        {
            try
            {
                nhibernateSession.SaveOrUpdate(obj);
            }
            catch (ConstraintViolationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void Excluir<T>(long id, bool useTransaction = true)
        {
            try
            {
                var obj = BuscarPorCodigo<T>(id);
                nhibernateSession.Delete(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
