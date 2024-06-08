using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Api_Pedidos.Classes_Modais;
using API_Pedidos.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PedidosDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodasOrigens",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirTodasOrigens");

// Mapeamento dos endpoints
app.MapGet("/", () => "");

// Registrar endpoints de pedidos
app.RegistrarEndpointsPedidos();

app.Run();
