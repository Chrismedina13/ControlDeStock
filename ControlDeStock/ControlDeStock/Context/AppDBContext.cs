using ControlDeStock.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ubicacion>().HasKey(ub => new { ub.CodUbicacion, ub.DepositoID });
            modelBuilder.Entity<UbicacionProducto>().HasKey(up => new { up.CodUbicacion, up.DepositoID , up.ProductoID});
        }

        public DbSet<Deposito> Deposito { get; set; }
        public DbSet<Ubicacion> Ubicacion { get; set; }
        public DbSet<UbicacionProducto> UbicacionProducto { get; set; }
    }
}
