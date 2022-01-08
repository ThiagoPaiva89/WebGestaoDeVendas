using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGestaoDeVendas.Models;

namespace WebGestaoDeVendas.Controllers
{
    public class DepartamentosController : Controller
    {
        public IActionResult Index()
        {
            List<Departamento> lista = new List<Departamento>();

            lista.Add(new Departamento { Id = 1, Nome = "Eletrônicos" });
            lista.Add(new Departamento { Id = 2, Nome = "Vestuário" });
            lista.Add(new Departamento { Id = 3, Nome = "Eletrodomésticos" });
            lista.Add(new Departamento { Id = 4, Nome = "Alimentos" });


            return View(lista);
        }
    }
}
