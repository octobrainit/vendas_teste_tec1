using Venda.Domain.Abstracoes;

namespace Venda.Domain.Entidades
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }

        public Cliente(Guid id, string nome)
        {
            Id = id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }
        public Cliente() { }
    }
}
