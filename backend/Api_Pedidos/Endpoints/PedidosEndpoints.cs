using Api_Pedidos.Classes_Modais;
using Api_Pedidos.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Pedidos.Endpoints
{
    public static class PedidosEndpoints
    {
        public static void RegistrarEndpointsPedidos(this IEndpointRouteBuilder rotas)
        {
            // Grupamento de rotas para /api/pedidos
            var rotaPedidos = rotas.MapGroup("/api/pedidos");

            // GET /api/pedidos
            rotaPedidos.MapGet("/", (PedidosDbContext db) =>
            {
                using var scope = rotas.ServiceProvider.CreateScope();

                var pedidos = db.Pedidos;

                return Results.Ok(pedidos);
            }).Produces<List<Pedido>>(); 

            // GET /api/pedidos/{id}
            // Retorna as informações de um pedido específico
            rotaPedidos.MapGet("/{id}", (int id) =>
            {
                using var scope = rotas.ServiceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<PedidosDbContext>();

                var pedido = db.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Produtos)
                        .ThenInclude(pp => pp.Produto)
                    .FirstOrDefault(p => p.Id_pedido == id);

                return pedido != null ? (IResult)Results.Ok(pedido) : Results.NotFound();
            }).Produces<Pedido>(); // Retorna um pedido específico

            // GET /api/pedidos/{id}/produtos
            // Retorna os produtos de um pedido específico
            rotaPedidos.MapGet("/{id}/produtos", (int id) =>
            {
                using var scope = rotas.ServiceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<PedidosDbContext>();

                var pedido = db.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Produtos)
                        .ThenInclude(pp => pp.Produto)
                    .FirstOrDefault(p => p.Id_pedido == id);

                return pedido != null ? (IResult)Results.Ok(pedido) : Results.NotFound();
            }).Produces<Pedido>(); 

            // POST /api/pedidos
            rotaPedidos.MapPost("/pedidos", (PedidosDTO pedidoDTO) =>
            {
                using var scope = rotas.ServiceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<PedidosDbContext>();

                List<PedidoProduto> pedidoProdutos = new List<PedidoProduto>();

                foreach (var produtoId in pedidoDTO.ProductsId)
                {
                    Produto produtoEncontrado = db.Produtos.FirstOrDefault(p => p.Id == produtoId);

                    PedidoProduto produto = new PedidoProduto
                    {
                        Produto = produtoEncontrado,
                        Quantidade = pedidoDTO.qtdeProduto
                    };

                    pedidoProdutos.Add(produto);
                }

                Pedido pedido = new Pedido
                {
                    Data_saida_pedido = pedidoDTO.Data_saida_pedido,
                    Data_entrada_pedido = pedidoDTO.Data_entrada_pedido,
                    StatusPedido = pedidoDTO.StatusPedido,
                    qtdeProduto = pedidoDTO.qtdeProduto,
                    Cliente = pedidoDTO.Cliente,
                    Produtos = pedidoProdutos,
                    FormaPagamento = pedidoDTO.FormaPagamento
                };

                try
                {
                    // Validar os dados recebidos
                    if (pedido == null)
                    {
                        return Results.BadRequest("Dados do pedido inválidos.");
                    }

                    // Adicionar o pedido ao contexto do banco de dados
                    db.Pedidos.Add(pedido);
                    db.SaveChanges();

                    // Retornar uma resposta de sucesso com o pedido adicionado
                    return Results.Created($"/api/pedidos/{pedido.Id_pedido}", pedido);
                }
                catch (Exception ex)
                {
                    // Em caso de erro, retornar uma resposta de erro
                    return Results.BadRequest($"Erro ao adicionar o pedido: {ex.Message}");
                }
            }).Produces<Pedido>(); 

            // PUT /api/pedidos/{id}
            // Atualiza um pedido existente
            rotaPedidos.MapPut("/{id}", (int id, Pedido pedido) =>
            {
                using var scope = rotas.ServiceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<PedidosDbContext>();
                var pedidoExistente = db.Pedidos.Find(id);
                if (pedidoExistente == null)
                {
                    return Results.NotFound();
                }

                db.Entry(pedidoExistente).CurrentValues.SetValues(pedido);
                db.SaveChanges();
                return Results.NoContent();
            });

            // DELETE /api/pedidos/{id}
            // Exclui um pedido existente
            rotaPedidos.MapDelete("/{id}", (int id) =>
            {
                using var scope = rotas.ServiceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<PedidosDbContext>();
                var pedido = db.Pedidos.Find(id);
                if (pedido == null)
                {
                    return Results.NotFound();
                }
                db.Pedidos.Remove(pedido);
                db.SaveChanges();
                return Results.NoContent();
            });
        }
    }
}
