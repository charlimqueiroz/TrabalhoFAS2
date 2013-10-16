using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadastroAluno.Domain;
using CadastroAluno.Domain.Business;
using CadastroAluno.Infra;
using CadastroAluno.Web.Controllers;
using CadastroAluno.Web.Models;

namespace CadastroAluno.Web.Controllers
{
    
    public class AlunoController : BaseController
    {

        #region Categoria de Aluno
        public ActionResult AlunoListar()
        {
            var lista = AlunoBusiness.ListarTodos();
            if (Request.IsAjaxRequest())
                return PartialView("_AlunoGrid", lista);
            return View(lista);
        }
        [HttpGet]
        public ActionResult AlunoInserir()
        {
            if (Request.IsAjaxRequest())
                return PartialView("_AlunoDados", new Aluno());
            return View();
        }
        [HttpPost]
        public ActionResult AlunoInserir(Aluno item)
        {
            try
            {
                AlunoBusiness.Salvar(item);
                if (Request.IsAjaxRequest())
                    return Json(new { Sucesso = true, Mensagem = "Registro inserido com sucesso" }, JsonRequestBehavior.AllowGet);

                return RedirectToAction("AlunoListar");
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { Success = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("AlunoListar");
            }
        }
        [HttpGet]
        public ActionResult AlunoEditar(string id)
        {
            try
            {
                var obj = AlunoBusiness.BuscarPorCodigo(long.Parse(Security.decrypt(id)));
                if (Request.IsAjaxRequest())
                    return PartialView("_AlunoDados", obj);
                return View(obj);
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { Success = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AlunoEditar(Aluno item)
        {
            try
            {
                AlunoBusiness.Atualizar(item);
                if (Request.IsAjaxRequest())
                    return Json(new { Sucesso = true }, JsonRequestBehavior.AllowGet);

                return RedirectToAction("AlunoListar");
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { Success = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("AlunoListar");
            }
        }
        public ActionResult AlunoExcluir(string id)
        {
            try
            {
                AlunoBusiness.Excluir(long.Parse(Security.decrypt(id)));
                if (Request.IsAjaxRequest())
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                return View();
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { Success = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        //[HttpGet]
        //public ActionResult MateriaAluno()
        //{
        //    var model = new AlunoModel()
        //    {
        //        Alunos = AlunoBusiness.ListarTodos(),
        //        Materias = MateriaBusiness.ListarTodos()
        //    };
        //    return View(model);
        //}
        //[HttpGet]
        //public ActionResult MateriaAluno()
        //{
        //    var model = new AlunoModel()
        //    {
        //        Alunos = AlunoBusiness.ListarTodos(),
        //        Materias = MateriaBusiness.ListarTodos()
        //    };
        //    return View(model);
        //}

        #endregion

     }
}
