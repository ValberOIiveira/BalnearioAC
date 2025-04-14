using BalnearioAC.Database;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Npgsql;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Configuração do logging
builder.Logging.AddConsole();

// Adiciona serviços
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configuração do banco de dados com suporte a timezone
builder.Services.AddDbContext<Conexao>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        o => 
        {
            o.UseNodaTime();
            
        });
});

var app = builder.Build();

// Middlewares na ordem CORRETA:

// Ativa o Swagger (documentação da API)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita CORS (antes de UseRouting)
app.UseCors("AllowAll");

// Arquivos estáticos
app.UseDefaultFiles();
app.UseStaticFiles();

// Roteamento e autorização
app.UseRouting();
app.UseAuthorization();

// Mapeamento de endpoints
app.MapControllers();

app.Run();