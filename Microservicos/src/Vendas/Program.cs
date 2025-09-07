using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Vendas.Data;
using System.Net.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Vendas.Configuration;

// --- Configuração da API ---
var builder = WebApplication.CreateBuilder(args);

builder
    .AddApiConfig()
    .AddCorsConfig()
    .AddSwaggerConfig()
    .AddIdentityConfig()
   ;
// Adiciona serviços ao contêiner.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Configuração do Entity Framework e do Banco de Dados ---
builder.Services.AddDbContext<VendasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona um cliente HTTP para comunicação com o serviço de estoque.
//builder.Services.AddHttpClient();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpClient("EstoqueHttpClient").ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                // Permite certificados autoassinados em ambiente de desenvolvimento.
                if (errors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors))
                {
                    return true;
                }
                return errors == SslPolicyErrors.None;
            }
        };
    });
}
else
{
    builder.Services.AddHttpClient("EstoqueHttpClient");
}
 
var app = builder.Build();

// --- Configuração do Pipeline de Requisição ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

// Inicializa o banco de dados e aplica as migrações ao iniciar a aplicação.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VendasDbContext>();
    context.Database.Migrate();
}

app.Run();
