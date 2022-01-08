using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebGestaoDeVendas.Models;

namespace WebGestaoDeVendas.Data
{
    public class WebGestaoDeVendasContext : DbContext
    {
        public WebGestaoDeVendasContext (DbContextOptions<WebGestaoDeVendasContext> options)
            : base(options)
        {
        }

        public DbSet<WebGestaoDeVendas.Models.Departamento> Departamento { get; set; }
    }
}
