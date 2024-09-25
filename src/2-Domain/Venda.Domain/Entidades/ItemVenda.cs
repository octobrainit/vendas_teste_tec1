using Venda.Domain.Abstracoes;

namespace Venda.Domain.Entidades
{
    public class ItemVenda : EntidadeBase
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal => (ValorUnitario * Quantidade) - Desconto;

        public ItemVenda(string produto, int quantidade, decimal valorUnitario, decimal desconto)
        {
            Id = Guid.NewGuid();
            Produto = produto ?? throw new ArgumentNullException(nameof(produto));
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            Desconto = desconto;
        }
        public ItemVenda() { }
    }
}
