using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Alteracoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_Produtos_produtoidProduto",
                table: "pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_categoriaidCategoria",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "categoriaidCategoria",
                table: "Produtos",
                newName: "CategoriaidCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_categoriaidCategoria",
                table: "Produtos",
                newName: "IX_Produtos_CategoriaidCategoria");

            migrationBuilder.RenameColumn(
                name: "produtoidProduto",
                table: "pedidos",
                newName: "ProdutoidProduto");

            migrationBuilder.RenameIndex(
                name: "IX_pedidos_produtoidProduto",
                table: "pedidos",
                newName: "IX_pedidos_ProdutoidProduto");

            migrationBuilder.RenameColumn(
                name: "uf",
                table: "Enderecos",
                newName: "Uf");

            migrationBuilder.RenameColumn(
                name: "logradouro",
                table: "Enderecos",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "localidade",
                table: "Enderecos",
                newName: "Localidade");

            migrationBuilder.RenameColumn(
                name: "cep",
                table: "Enderecos",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "bairro",
                table: "Enderecos",
                newName: "Bairro");

            migrationBuilder.RenameColumn(
                name: "telefone",
                table: "Cozinhas",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "avaliacao",
                table: "Cozinhas",
                newName: "Avaliacao");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_Produtos_ProdutoidProduto",
                table: "pedidos",
                column: "ProdutoidProduto",
                principalTable: "Produtos",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_CategoriaidCategoria",
                table: "Produtos",
                column: "CategoriaidCategoria",
                principalTable: "Categorias",
                principalColumn: "idCategoria",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_Produtos_ProdutoidProduto",
                table: "pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_CategoriaidCategoria",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "CategoriaidCategoria",
                table: "Produtos",
                newName: "categoriaidCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_CategoriaidCategoria",
                table: "Produtos",
                newName: "IX_Produtos_categoriaidCategoria");

            migrationBuilder.RenameColumn(
                name: "ProdutoidProduto",
                table: "pedidos",
                newName: "produtoidProduto");

            migrationBuilder.RenameIndex(
                name: "IX_pedidos_ProdutoidProduto",
                table: "pedidos",
                newName: "IX_pedidos_produtoidProduto");

            migrationBuilder.RenameColumn(
                name: "Uf",
                table: "Enderecos",
                newName: "uf");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Enderecos",
                newName: "logradouro");

            migrationBuilder.RenameColumn(
                name: "Localidade",
                table: "Enderecos",
                newName: "localidade");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Enderecos",
                newName: "cep");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Enderecos",
                newName: "bairro");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Cozinhas",
                newName: "telefone");

            migrationBuilder.RenameColumn(
                name: "Avaliacao",
                table: "Cozinhas",
                newName: "avaliacao");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_Produtos_produtoidProduto",
                table: "pedidos",
                column: "produtoidProduto",
                principalTable: "Produtos",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_categoriaidCategoria",
                table: "Produtos",
                column: "categoriaidCategoria",
                principalTable: "Categorias",
                principalColumn: "idCategoria",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
