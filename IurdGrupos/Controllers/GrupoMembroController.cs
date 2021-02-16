using IurdGrupos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IurdGrupos.Controllers
{
    public class GrupoMembroController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Lista = new GrupoMembroModel().RetornarListagem();
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(GrupoMembroModel grupo)
        {
            if(ModelState.IsValid)
            {
                grupo.Inserir();
                CarregarDados();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }           
        }

        [HttpGet]
        public IActionResult Filtro()
        {
            ViewBag.Lista = new GrupoMembroModel().RetornarListagem();
            return View();
        }

        [HttpPost]
        public IActionResult Filtro(GrupoMembroModel filtro)
        {
            try
            {
                String grupoNome = filtro.GrupoNome.ToString();
                ViewBag.Lista = new GrupoMembroModel().RetornarListagemNome(grupoNome);
                return View();
            }
            catch(Exception)
            {
                return View();
            }              
        }

        public IActionResult Excluir(int?Id)
        {
            ViewData["IdExcluir"] = Id;
            return View();
        }

        public IActionResult ExcluirUsuario(int? Id)
        {
            try
            {
                new GrupoMembroModel().Excluir(Id);
                return View();
            }
            catch(Exception)
            {
                return View();
            }
        }

        private void CarregarDados()
        {
            ViewBag.ListaGrupos = new GrupoMembroModel().RetornarListaGrupo();
            ViewBag.ListaMembros = new GrupoMembroModel().RetornarListaMembro();
        }
    }
}
