using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGestaoDeVendas.Data;
using WebGestaoDeVendas.Models;
using Microsoft.EntityFrameworkCore;

namespace WebGestaoDeVendas.Services
{
    public class RegistroDeVendasService
    {
        private readonly WebGestaoDeVendasContext _context;

        public RegistroDeVendasService(WebGestaoDeVendasContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroDeVendas>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.RegistroDeVendas select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }

            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }

    }
}
