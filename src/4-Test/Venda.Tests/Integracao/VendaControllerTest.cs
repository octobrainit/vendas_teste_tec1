using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Venda.API.DTO;
using Venda.Data.Contexto;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Venda.Tests.Integracao
{
    public class VendaControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public VendaControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("VendasDB_Test"));
                });
            }).CreateClient();
        }

        [Fact]
        public async Task CriarVenda_DeveRetornarCreated()
        {
            var faker = new Faker("pt_BR");
            var vendaDto = new VendaDTO
            {
                Data = DateTime.UtcNow,
                Cliente = new ClienteDTO { Id = Guid.NewGuid(), Nome = faker.Person.FullName },
                Filial = faker.Company.CompanyName(),
                Itens =
                        [
                            new() {
                                Produto = faker.Name.FullName(),
                                Quantidade = 2,
                                ValorUnitario = 50,
                                Desconto = 5
                            }
                        ]
            };

            var response = await _client.PostAsJsonAsync("/api/venda", vendaDto);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
