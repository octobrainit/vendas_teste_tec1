using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Venda.Data.Contexto;
using Venda.Data.Repositorios.V1;
using Venda.Domain.Interfaces.V1.Repositorios;

namespace Venda.Data.IoC
{
    public static class InversaoDeDependencia
    {
        public static IServiceCollection AdicionarRepositorios(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVendaRepositorio, VendaRepositorio>();

            services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("postgres")));

            return services;
        }
    }
}
