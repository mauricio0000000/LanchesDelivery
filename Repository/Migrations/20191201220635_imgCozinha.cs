using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class imgCozinha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_Produtos_ProdutoidProduto",
                table: "pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pedidos",
                table: "pedidos");

            migrationBuilder.RenameTable(
                name: "pedidos",
                newName: "itemPedidos");

            migrationBuilder.RenameIndex(
                name: "IX_pedidos_ProdutoidProduto",
                table: "itemPedidos",
                newName: "IX_itemPedidos_ProdutoidProduto");

            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Cozinhas",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemPedidos",
                table: "itemPedidos",
                column: "idPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_itemPedidos_Produtos_ProdutoidProduto",
                table: "itemPedidos",
                column: "ProdutoidProduto",
                principalTable: "Produtos",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemPedidos_Produtos_ProdutoidProduto",
                table: "itemPedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itemPedidos",
                table: "itemPedidos");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Cozinhas");

            migrationBuilder.RenameTable(
                name: "itemPedidos",
                newName: "pedidos");

            migrationBuilder.RenameIndex(
                name: "IX_itemPedidos_ProdutoidProduto",
                table: "pedidos",
                newName: "IX_pedidos_ProdutoidProduto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pedidos",
                table: "pedidos",
                column: "idPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_Produtos_ProdutoidProduto",
                table: "pedidos",
                column: "ProdutoidProduto",
                principalTable: "Produtos",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
