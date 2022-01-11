using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGestaoDeVendas.Services;

namespace WebGestaoDeVendas.Controllers
{
    public class VendedoresController : Controller
    {

        private readonly VendedorService _vendedorService;

        public VendedoresController(VendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }

        public IActionResult Index()
        {

            var lista = _vendedorService.FindAll();

            return View(lista);
        }
    }
}
