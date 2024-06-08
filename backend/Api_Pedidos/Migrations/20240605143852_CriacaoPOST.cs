using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Pedidos.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoPOST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtdeProduto",
                table: "Pedidos",
                newName: "qtdeProduto");

            migrationBuilder.RenameColumn(
                name: "Status_pedido",
                table: "Pedidos",
                newName: "StatusPedido");

            migrationBuilder.RenameColumn(
                name: "Forma_pagamento",
                table: "Pedidos",
                newName: "FormaPagamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "qtdeProduto",
                table: "Pedidos",
                newName: "QtdeProduto");

            migrationBuilder.RenameColumn(
                name: "StatusPedido",
                table: "Pedidos",
                newName: "Status_pedido");

            migrationBuilder.RenameColumn(
                name: "FormaPagamento",
                table: "Pedidos",
                newName: "Forma_pagamento");
        }
    }
}
