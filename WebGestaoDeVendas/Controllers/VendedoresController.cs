using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGestaoDeVendas.Services;
using WebGestaoDeVendas.Models;
using WebGestaoDeVendas.Models.ViewModels;
using WebGestaoDeVendas.Services.Exceptions;
using System.Diagnostics;

namespace WebGestaoDeVendas.Controllers
{
    public class VendedoresController : Controller
    {

        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public IActionResult Index()
        {

            var lista = _vendedorService.FindAll();

            return View(lista);
        }

        public IActionResult Create()
        {
            var departamentos = _departamentoService.FindAll();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamento = _departamentoService.FindAll();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento };
                return View(viewModel);
            }
            _vendedorService.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não fornecido"});
            }

            var obj = _vendedorService.FindById(id.Value);

            if(obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado" });
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletar(int id)
        {
            _vendedorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não fornecido" });
            }

            var obj = _vendedorService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        public IActionResult Editar(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não fornecido" });
            }

            var obj = _vendedorService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado" });
            }

            List<Departamento> departamentos = _departamentoService.FindAll();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamento = _departamentoService.FindAll();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento};
                return View(viewModel);
            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id's não correspondem" });
            }
            try
            {
                _vendedorService.Update(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Erro), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Erro), new { message = e.Message });
            }
           

        }

        public IActionResult Erro(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
