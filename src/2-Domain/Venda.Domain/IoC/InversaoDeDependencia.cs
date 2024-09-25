using Microsoft.Extensions.DependencyInjection;
using Venda.Domain.Interfaces.V1.Servicos;
using Venda.Domain.Servicos.V1;

namespace Venda.Domain.IoC
{
    public static class InversaoDeDependencia
    {
        public static IServiceCollection AdicionarServicoDominio(this IServiceCollection services)
        {
            services.AddScoped<IVendaServico<Entidades.Venda>, VendaServico>();
            return services;
        }
    }
}
