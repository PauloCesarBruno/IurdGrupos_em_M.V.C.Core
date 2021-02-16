using IurdGrupos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IurdGrupos.Controllers
{
    public class MembroController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaMembro = new ModelMembro().ListarTodosMembros();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (Id != null)
            {
                ViewBag.Membros = new ModelMembro().RetornarMembroId(Id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ModelMembro membro)
        {
            if (ModelState.IsValid)
            {
                membro.GravarMembro();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Excluir(int? Id)
        {
            ViewData["IdExcluir"] = Id;
            return View();
        }

        public IActionResult ExcluirMembro(int? Id)
        {
            try
            {
                new ModelMembro().Excluir(Id);
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Filtro()
        {
            ViewBag.ListaMembro = new ModelMembro().ListarTodosMembros();
            return View();
        }

        [HttpPost]
        public IActionResult Filtro(ModelMembro filtro)
        {
            try
            {
                String nome = filtro.Nome.ToString();
                ViewBag.ListaMembro = new ModelMembro().ListarTodosMembrosNome(nome);
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
