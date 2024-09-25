namespace Venda.Domain.Eventos.V1
{
    public class ItemCanceladoEvento
    {
        public Guid VendaId { get; set; }
        public Guid ItemId { get; set; }
    }
}
