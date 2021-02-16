using IurdGrupos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IurdGrupos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(int? Id)
        {
            if (Id != null)
            {
                if (Id == 0)
                {
                    HttpContext.Session.SetString("IdUsuario", String.Empty);
                    HttpContext.Session.SetString("NomeUsuario", String.Empty);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(ModelLogin login)
        {
            if (ModelState.IsValid)
            {
                Boolean LoginOk = login.ValidarLogin();
                if (LoginOk)
                {
                    HttpContext.Session.SetString("IdUsuario", login.Id);
                    HttpContext.Session.SetString("NomeUsuario", login.Nome);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Email e/ou Senha Incorretos !";
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
