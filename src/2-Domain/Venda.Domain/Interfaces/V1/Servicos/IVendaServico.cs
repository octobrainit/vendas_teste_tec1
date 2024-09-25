namespace Venda.Domain.Interfaces.V1.Servicos
{
    public interface IVendaServico<T> where T : Entidades.Venda
    {
        Task<T> CriarVendaAsync(T venda);
        Task<IEnumerable<T>> BuscarTodasAsVendasAsync();
        Task<T> AlterarVendaAsync(T venda);
        Task CancelarVendaAsync(Guid id);
        Task<T> ObterVendaPorIdAsync(Guid id);
    }
}
