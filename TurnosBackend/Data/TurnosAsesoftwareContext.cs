using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TurnosAsesoftwareContext : DbContext
    {
        public TurnosAsesoftwareContext(DbContextOptions<TurnosAsesoftwareContext> options):base(options)
        {

        }

       public DbSet<Cliente> Clientes { get; set; }
       public DbSet<Direccion> Direcciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Direccion>().ToTable("Direccion");
        }
    }
}
