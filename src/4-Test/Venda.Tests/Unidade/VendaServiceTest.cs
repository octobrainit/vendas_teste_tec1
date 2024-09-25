using Bogus;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Venda.Domain.Entidades;
using Venda.Domain.Interfaces.V1.Repositorios;
using Venda.Domain.Servicos.V1;

namespace Venda.Tests.Unidade
{
    public class VendaServiceTest
    {
        private readonly IVendaRepositorio _vendaRepository;
        private readonly ILogger<VendaServico> _logger;
        private readonly VendaServico _vendaService;
        private readonly IPublishEndpoint _publishMessage;

        public VendaServiceTest()
        {
            _vendaRepository = Substitute.For<IVendaRepositorio>();
            _logger = Substitute.For<ILogger<VendaServico>>();
            _publishMessage = Substitute.For<IPublishEndpoint>();
            _vendaService = new VendaServico(_vendaRepository, _logger, _publishMessage);
        }

        [Fact]
        public async Task CriarVenda_DeveAdicionarVenda()
        {
            var venda = GerarVenda();

            var result = await _vendaService.CriarVendaAsync(venda);

            await _vendaRepository.Received(1).AdicionarAsync(venda);
            result.Should().BeEquivalentTo(venda);
        }

        private Domain.Entidades.Venda GerarVenda()
        {
            var faker = new Faker("pt_BR");
            var cliente = new Cliente(Guid.NewGuid(), faker.Person.FullName);
            var venda = new Domain.Entidades.Venda(DateTime.Now, cliente, faker.Company.CompanyName());

            for (int i = 0; i < 3; i++)
            {
                var item = new ItemVenda(
                    faker.Commerce.ProductName(),
                    faker.Random.Int(1, 10),
                    faker.Random.Decimal(10, 100),
                    faker.Random.Decimal(0, 10));
                venda.AdicionarItem(item);
            }

            return venda;
        }
    }
}
