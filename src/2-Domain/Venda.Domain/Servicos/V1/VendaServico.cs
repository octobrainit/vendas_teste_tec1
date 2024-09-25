using MassTransit;
using Microsoft.Extensions.Logging;
using Venda.Domain.Eventos.V1;
using Venda.Domain.Interfaces.V1.Repositorios;
using Venda.Domain.Interfaces.V1.Servicos;

namespace Venda.Domain.Servicos.V1
{
    public class VendaServico : IVendaServico<Entidades.Venda>
    {
        private readonly IVendaRepositorio _vendaRepository;
        private readonly ILogger<VendaServico> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public VendaServico(
            IVendaRepositorio vendaRepository,
            ILogger<VendaServico> logger,
            IPublishEndpoint publishEndpoint)
        {
            _vendaRepository = vendaRepository;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Entidades.Venda> AlterarVendaAsync(Entidades.Venda venda)
        {
            await _vendaRepository.AtualizarAsync(venda);
            _logger.LogInformation("Evento: CompraAlterada para Venda ID: {VendaId}", venda.Id);

            var evento = new CompraAlteradaEvento
            {
                VendaId = venda.Id,
            };

            await _publishEndpoint.Publish(evento);

            return venda;
        }

        public async Task CancelarVendaAsync(Guid id)
        {
            var venda = await _vendaRepository.ObterPorIdAsync(id);
            if (venda == null)
            {
                throw new KeyNotFoundException("Venda não encontrada.");
            }

            venda.Cancelar();
            await _vendaRepository.AtualizarAsync(venda);
            _logger.LogInformation("Evento: CompraCancelada para Venda ID: {VendaId}", venda.Id);

            var evento = new CompraCanceladaEvento
            {
                VendaId = venda.Id,
            };

            await _publishEndpoint.Publish(evento);
        }

        public async Task<Entidades.Venda> CriarVendaAsync(Entidades.Venda venda)
        {
            await _vendaRepository.AdicionarAsync(venda);

            _logger.LogInformation("Evento: CompraCriada para Venda ID: {VendaId}", venda.Id);

            var evento = new CompraCriadaEvento
            {
                VendaId = venda.Id,
                Data = venda.Data,
            };

            await _publishEndpoint.Publish(evento);

            return venda;
        }

        public async Task<Entidades.Venda> ObterVendaPorIdAsync(Guid id)
        {
            return await _vendaRepository.ObterPorIdAsync(id);
        }
        public async Task<IEnumerable<Entidades.Venda>> BuscarTodasAsVendasAsync()
        {
            return await _vendaRepository.ObterTodasAsync();
        }
    }
}
