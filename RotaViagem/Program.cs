using Microsoft.EntityFrameworkCore;
using RotaViagem.Business;
using RotaViagem.Context;
using RotaViagem.Controllers;
using RotaViagem.Interface;
using RotaViagem.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Configuração do contexto de dados
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<RotasController>();

//builder.Services.AddTransient<RotasController>();
builder.Services.AddScoped<IRotaImplementation, RotaImplementation>();
builder.Services.AddScoped<IRotaBusiness, RotaBusiness>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
