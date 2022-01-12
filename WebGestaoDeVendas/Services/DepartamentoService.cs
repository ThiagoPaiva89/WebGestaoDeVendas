using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGestaoDeVendas.Data;
using WebGestaoDeVendas.Models;

namespace WebGestaoDeVendas.Services
{
    public class DepartamentoService
    {
        private readonly WebGestaoDeVendasContext _context;

        public DepartamentoService(WebGestaoDeVendasContext context)
        {
            _context = context;
        }

        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(x => x.Nome).ToList();
        }
    }
}
