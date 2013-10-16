using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastroAluno.Web.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult Decripty(string id)
        {
            return Json(new { id = CadastroAluno.Infra.Security.decrypt(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Encrypt(string id)
        {
            return Json(new { id = CadastroAluno.Infra.Security.encrypt(id) }, JsonRequestBehavior.AllowGet);
        }

    }
}
