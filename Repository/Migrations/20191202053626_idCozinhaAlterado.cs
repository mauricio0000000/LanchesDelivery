using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class idCozinhaAlterado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Cozinhas_CozinhaidCozinha",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "CozinhaidCozinha",
                table: "Produtos",
                newName: "cozinhaidCozinha");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_CozinhaidCozinha",
                table: "Produtos",
                newName: "IX_Produtos_cozinhaidCozinha");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Cozinhas_cozinhaidCozinha",
                table: "Produtos",
                column: "cozinhaidCozinha",
                principalTable: "Cozinhas",
                principalColumn: "idCozinha",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Cozinhas_cozinhaidCozinha",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "cozinhaidCozinha",
                table: "Produtos",
                newName: "CozinhaidCozinha");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_cozinhaidCozinha",
                table: "Produtos",
                newName: "IX_Produtos_CozinhaidCozinha");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Cozinhas_CozinhaidCozinha",
                table: "Produtos",
                column: "CozinhaidCozinha",
                principalTable: "Cozinhas",
                principalColumn: "idCozinha",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
