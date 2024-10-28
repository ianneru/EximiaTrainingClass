using EscolaEximia.HttpService.Dominio.Inscricoes.Infra;
using EscolaEximia.HttpService.Handlers;
using HttpService.Dominio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PropostaDBContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("InscricoesConnection")));
builder.Services.AddScoped<CriarPropostaHandler>();
builder.Services.AddScoped<PropostaRepositorio>();
builder.Services.AddScoped<AgenteRepositorio>();
builder.Services.AddScoped<ClienteRepositorio>();
builder.Services.AddScoped<ConvenioRepositorio>();
//builder.Services.AddHostedService<DatabaseInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
