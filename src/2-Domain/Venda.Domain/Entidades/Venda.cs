using Venda.Domain.Abstracoes;

namespace Venda.Domain.Entidades
{
    public class Venda : EntidadeBase
    {
        public DateTime Data { get; set; }
        public Cliente Cliente { get; set; }
        public decimal ValorTotal => Itens.Sum(item => item.ValorTotal);
        public string Filial { get; set; }
        public IReadOnlyCollection<ItemVenda> Itens => _itens.AsReadOnly();
        public bool Cancelado { get; set; }

        private List<ItemVenda> _itens = new List<ItemVenda>();

        public Venda(DateTime data, Cliente cliente, string filial)
        {
            Id = Guid.NewGuid();
            Data = data;
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Filial = filial ?? throw new ArgumentNullException(nameof(filial));
        }
        public Venda() { }

        public void AdicionarItem(ItemVenda item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _itens.Add(item);
        }

        public void Cancelar()
        {
            Cancelado = true;
        }
    }
}
