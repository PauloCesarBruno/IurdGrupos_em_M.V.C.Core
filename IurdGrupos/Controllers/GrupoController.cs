using IurdGrupos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IurdGrupos.Controllers
{
    public class GrupoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaGrupo = new ModelGrupo().ListarTodosGrupos();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (Id != null)
            {
                ViewBag.Grupos = new ModelGrupo().RetornarGrupoId(Id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ModelGrupo grupo)
        {
            if(ModelState.IsValid)
            {
                grupo.GravarGrupo();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Excluir(int? Id)
        {
            ViewData["IdExcluir"] = Id;
            return View();
        }

        public IActionResult ExcluirGrupo(int? Id)
        {
            try
            {
                new ModelGrupo().Excluir(Id);
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
            ViewBag.ListaGrupo = new ModelGrupo().ListarTodosGrupos();
            return View();
        }

        [HttpPost]
        public IActionResult Filtro(ModelGrupo filtro)
        {
            try
            {
                String nome = filtro.GrupoNome.ToString();
                ViewBag.ListaGrupo = new ModelGrupo().ListarTodosGruposNome(nome);
                return View();
            }
            catch (Exception)
            {
                return View();
            }                
        }
    }
}
