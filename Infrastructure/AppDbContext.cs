using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<Abono> Abonos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // Agrega un constructor vacío solo para `dotnet ef`
        public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Creditos)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId);

            modelBuilder.Entity<Credito>()
                .HasMany(c => c.Abonos)
                .WithOne(a => a.Credito)
                .HasForeignKey(a => a.CreditoId);
        }
    }
}
