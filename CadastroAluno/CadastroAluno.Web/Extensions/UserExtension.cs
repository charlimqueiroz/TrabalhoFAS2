using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using CadastroAluno.Domain.Business;
using CadastroAluno.Infra;

namespace CadastroAluno.Web.Extensions
{
    public static class UserExtension
    {
//        public static bool HasPermission(this IPrincipal user, string token)
//        {
//#if DEBUG
//            return true;
//#endif
//            if (string.IsNullOrEmpty(user.Identity.Name)) // não logado
//                throw new UnauthorizedAccessException();
//            var usuario = UsuarioBusiness.BuscarPorCodigo(long.Parse(Security.decrypt(user.Identity.Name)));
//            return usuario.Perfil.Funcionalidades.Any(f => f.Token == token);
//        }
//        public static bool HasPermission(this IPrincipal user)
//        {
//            if (string.IsNullOrEmpty(user.Identity.Name)) // não logado
//                throw new UnauthorizedAccessException();

//            return true;
//        }

    }
}