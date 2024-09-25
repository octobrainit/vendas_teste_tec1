using Microsoft.EntityFrameworkCore;
using Venda.Data.Contexto;
using Venda.Domain.Interfaces.V1.Repositorios;

namespace Venda.Data.Repositorios.V1
{
    public class VendaRepositorio : IVendaRepositorio
    {
        private readonly AppDbContext _context;

        public VendaRepositorio(AppDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(Domain.Entidades.Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Domain.Entidades.Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entidades.Venda> ObterPorIdAsync(Guid id)
        {
            return await _context.Vendas
               .Include(v => v.Cliente)
               .Include(v => v.Itens)
               .FirstOrDefaultAsync(v => v.Id == id) ?? new();
        }

        public async Task<IEnumerable<Domain.Entidades.Venda>> ObterTodasAsync()
        {
            return await _context.Vendas
               .Include(v => v.Cliente)
               .Include(v => v.Itens)
               .ToListAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var venda = await ObterPorIdAsync(id);
            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
        }
    }
}
