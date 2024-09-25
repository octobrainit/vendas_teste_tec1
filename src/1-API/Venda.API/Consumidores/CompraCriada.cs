using MassTransit;
using Venda.Domain.Eventos.V1;

namespace Venda.API.Consumidores
{
    public class CompraCriada : IConsumer<CompraCriadaEvento>
    {
        private readonly ILogger<CompraCriada> _logger;

        public CompraCriada(ILogger<CompraCriada> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CompraCriadaEvento> context)
        {
            var message = context.Message;
            _logger.LogInformation("CompraCriadaEvent recebido: VendaId = {VendaId}", message.VendaId);
            return Task.CompletedTask;
        }
    }
}
