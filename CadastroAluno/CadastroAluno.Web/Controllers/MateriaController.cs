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
    
    public class MateriaController : BaseController
    {

        #region Categoria de Materia
        public ActionResult MateriaListar()
        {
            var lista = MateriaBusiness.ListarTodos();
            if (Request.IsAjaxRequest())
                return PartialView("_MateriaGrid", lista);
            return View(lista);
        }
        [HttpGet]
        public ActionResult MateriaInserir()
        {
            if (Request.IsAjaxRequest())
                return PartialView("_MateriaDados", new Materia());
            return View();
        }
        [HttpPost]
        public ActionResult MateriaInserir( Materia item)
        {
            try
            {
                MateriaBusiness.Salvar(item);
                if (Request.IsAjaxRequest())
                    return Json(new { Sucesso = true, Mensagem = "Registro inserido com sucesso" }, JsonRequestBehavior.AllowGet);

                return RedirectToAction("MateriaListar");
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { Success = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("MateriaListar");
            }
        }
        [HttpGet]
        public ActionResult MateriaEditar(string id)
        {
            try
            {
                var obj = MateriaBusiness.BuscarPorCodigo(long.Parse(Security.decrypt(id)));
                if (Request.IsAjaxRequest())
                    return PartialView("_MateriaDados", obj);
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
        public ActionResult MateriaEditar( Materia item)
        {
            try
            {
                MateriaBusiness.Atualizar(item);
                if (Request.IsAjaxRequest())
                    return Json(new { Sucesso = true }, JsonRequestBehavior.AllowGet);

                return RedirectToAction("MateriaListar");
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { Success = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("MateriaListar");
            }
        }
        public ActionResult MateriaExcluir(string id)
        {
            try
            {
                MateriaBusiness.Excluir(long.Parse(Security.decrypt(id)));
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
      
        #endregion

     }
}
