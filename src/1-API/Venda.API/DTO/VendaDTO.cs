namespace Venda.API.DTO
{
    public class VendaDTO
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public ClienteDTO Cliente { get; set; }
        public decimal ValorTotal { get; set; }
        public string Filial { get; set; }
        public List<ItemVendaDTO> Itens { get; set; }
        public bool Cancelado { get; set; }
    }
}
