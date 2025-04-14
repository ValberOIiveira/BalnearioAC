using BalnearioAC.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//criar a aplicação web

builder.Services.AddControllers();
//Adiciona o serviço de controllers

builder.Services.AddControllersWithViews();
//Adiciona o serviço de controllers com views

builder.Services.AddEndpointsApiExplorer();
//Adiciona o serviço de explorador de endpoints

builder.Services.AddSwaggerGen();
//Adiciona o serviço de gerador de Swagger

builder.Services.AddDbContext<Conexao>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});//Adiciona o serviço de banco de dados

var app = builder.Build(); //vai construir a aplicação
if(app.Environment.IsDevelopment())
{
    app.UseSwagger(); //Vai usar o Swagger
    app.UseSwaggerUI(); //Vai usar o Swagger UI
    app.UseHttpsRedirection(); //Vai usar o HTTPS
    app.UseAuthorization(); //Vai usar a autorização
}

app.UseDefaultFiles(); //Vai usar o arquivo padrão
app.UseStaticFiles(); //Vai usar o arquivo estático
app.UseHttpsRedirection();
app.MapControllers(); //Var mapear os controladores
app.Run(); //Vai rodar a aplicação