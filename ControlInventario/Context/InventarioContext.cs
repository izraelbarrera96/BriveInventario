using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Data
{
    public class InventarioContext : DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Existencia> Existencias { get; set; }

    }
}