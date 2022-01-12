using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGestaoDeVendas.Data;
using WebGestaoDeVendas.Models;
using Microsoft.EntityFrameworkCore;

namespace WebGestaoDeVendas.Services
{
    public class DepartamentoService
    {
        private readonly WebGestaoDeVendasContext _context;

        public DepartamentoService(WebGestaoDeVendasContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
