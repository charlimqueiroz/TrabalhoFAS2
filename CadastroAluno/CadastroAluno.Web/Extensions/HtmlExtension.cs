using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastroAluno.Web.Extensions
{
    public static class HtmlExtension
    {
        public static bool HasPermission(this HtmlHelper htmlHelper, string token)
        {
            //return HttpContext.Current.User.HasPermission(token);
            return true;
        }
    }
}