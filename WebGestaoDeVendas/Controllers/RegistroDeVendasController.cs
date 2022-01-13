using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGestaoDeVendas.Services;

namespace WebGestaoDeVendas.Controllers
{
    public class RegistroDeVendasController : Controller
    {
        private readonly RegistroDeVendasService _registroDeVendasService;

        public RegistroDeVendasController(RegistroDeVendasService registroDeVendasService)
        {
            _registroDeVendasService = registroDeVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BuscaSimples(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _registroDeVendasService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public IActionResult BuscaAgrupada()
        {
            return View();
        }
    }
}
