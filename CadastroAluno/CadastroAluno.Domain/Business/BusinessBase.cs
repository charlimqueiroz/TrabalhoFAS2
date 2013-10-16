using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroAluno.Domain.Business
{
    public class BusinessBase<T> where T : EntityBase<T>
    {
        public static void Salvar(T item)
        {
            Dao.CommonDao.Salvar(item);
        }
        public static void Atualizar(T item)
        {
            var oldItem = BuscarPorCodigo(item.ID);

            foreach (var prop in oldItem.GetType().GetProperties())
            {
                try
                {
                    if (!prop.PropertyType.Name.Contains("IList"))
                        prop.SetValue(oldItem, item.GetType().GetProperty(prop.Name).GetValue(item, null), null);
                }
                catch (System.Exception) { }
            }
            Dao.CommonDao.Atualizar(oldItem);
        }
        public static void Excluir(long id)
        {
            Dao.CommonDao.Excluir<T>(id);
        }
        public static IList<T> ListarTodos()
        {
            return Dao.CommonDao.BuscarTodos<T>();
        }
        public static T BuscarPorCodigo(long id)
        {
            return Dao.CommonDao.BuscarPorCodigo<T>(id);
        }
        public static T BuscarPorPropriedade(string propertyName, object valor)
        {
            return Dao.CommonDao.BuscarPorPropriedade<T>(propertyName, valor);
        }
        public static IList<T> ListarPorPropriedade(string propertyName, object valor)
        {
            return Dao.CommonDao.ListarPorPropriedade<T>(propertyName, valor);
        }
    }
}
