using System.Text.Json.Serialization;
using Estoque.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// ---Configura��o da API ---
var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Configura��o do Entity Framework e do Banco de Dados ---
builder.Services.AddDbContext<EstoqueDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Configura��o de Autentica��o JWT (Placeholder) ---
// Em um sistema real, voc� configuraria a valida��o do token JWT aqui.
// Ex: builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)...

var app = builder.Build();

// --- Configura��o do Pipeline de Requisi��o ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inicializa o banco de dados e aplica as migra��es ao iniciar a aplica��o.
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<EstoqueDbContext>();
//    context.Database.Migrate();
//}

app.Run();