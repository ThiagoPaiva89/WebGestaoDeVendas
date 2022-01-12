using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGestaoDeVendas.Models;
using WebGestaoDeVendas.Data;

namespace WebGestaoDeVendas.Services
{
    public class VendedorService
    {
        private readonly WebGestaoDeVendasContext _context;

        public VendedorService(WebGestaoDeVendasContext context)
        {
            _context = context;
        }

        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.ToList();
        }

        public void Insert(Vendedor obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
