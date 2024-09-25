using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Venda.API.Consumidores;
using Venda.Data.Contexto;

namespace Venda.API.IoC
{
    public static class InversaoDeDependencia
    {
        public static IServiceCollection AdicionarMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CompraCriada>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("rabbitMqHost"));

                    cfg.ReceiveEndpoint("compra-criada-queue", e =>
                    {
                        e.ConfigureConsumer<CompraCriada>(context);
                    });
                });
            });
            return services;
        }

        public static ConfigureHostBuilder ConfigurarSerilog(this ConfigureHostBuilder configureHostBuilder)
        {
            configureHostBuilder.UseSerilog((context, configuration) =>
                                    configuration.ReadFrom.Configuration(context.Configuration));

            return configureHostBuilder;
        }

        public static WebApplication ValidarMigrations(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
            }

            return webApplication;
        }

        public static IServiceCollection AdicionarControladores(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}
