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

        public async Task<IActionResult> Index()
        {

            var lista = await _vendedorService.FindAllAsync();

            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoService.FindAllAsync();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamento = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento };
                return View(viewModel);
            }
            await _vendedorService.InsertAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não fornecido" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado" });
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _vendedorService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Erro), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não fornecido" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não fornecido" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado" });
            }

            List<Departamento> departamentos = await _departamentoService.FindAllAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamento = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento };
                return View(viewModel);
            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id's não correspondem" });
            }
            try
            {
                await _vendedorService.UpdateAsync(vendedor);
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
