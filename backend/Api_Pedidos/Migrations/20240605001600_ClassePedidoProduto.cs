using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Pedidos.Migrations
{
    /// <inheritdoc />
    public partial class ClassePedidoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Pedidos_PedidoId_pedido",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_PedidoId_pedido",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PedidoId_pedido",
                table: "Produtos");

            migrationBuilder.CreateTable(
                name: "PedidoProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<double>(type: "double", nullable: false),
                    PedidoId_pedido = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoProduto_Pedidos_PedidoId_pedido",
                        column: x => x.PedidoId_pedido,
                        principalTable: "Pedidos",
                        principalColumn: "Id_pedido");
                    table.ForeignKey(
                        name: "FK_PedidoProduto_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProduto_PedidoId_pedido",
                table: "PedidoProduto",
                column: "PedidoId_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProduto_ProdutoId",
                table: "PedidoProduto",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoProduto");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId_pedido",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_PedidoId_pedido",
                table: "Produtos",
                column: "PedidoId_pedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Pedidos_PedidoId_pedido",
                table: "Produtos",
                column: "PedidoId_pedido",
                principalTable: "Pedidos",
                principalColumn: "Id_pedido");
        }
    }
}
