using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGestaoDeVendas.Controllers
{
    public class RegistroDeVendasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BuscaSimples()
        {
            return View();
        }

        public IActionResult BuscaAgrupada()
        {
            return View();
        }
    }
}
