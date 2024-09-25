namespace Venda.Domain.Interfaces.V1.Repositorios
{
    public interface IVendaRepositorio
    {
        Task<Entidades.Venda> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Entidades.Venda>> ObterTodasAsync();
        Task AdicionarAsync(Entidades.Venda venda);
        Task AtualizarAsync(Entidades.Venda venda);
        Task RemoverAsync(Guid id);
    }
}
