using Venda.API.IoC;
using Venda.Data.IoC;
using Venda.Domain.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigurarSerilog();
builder.Services
       .AdicionarControladores()
       .AdicionarServicoDominio()
       .AdicionarRepositorios(builder.Configuration)
       .AdicionarMassTransit(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
   .UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }