using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Venda.Domain.Entidades;

namespace Venda.Data.Contexto
{
    public class AppDbContext : DbContext
    {
        public DbSet<Domain.Entidades.Venda> Vendas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
