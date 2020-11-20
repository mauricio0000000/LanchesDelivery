using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class idCozinhaAlteradoNovo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Cozinhas_cozinhaidCozinha",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_cozinhaidCozinha",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "cozinhaidCozinha",
                table: "Produtos");

            migrationBuilder.AddColumn<int>(
                name: "idCozinha",
                table: "Produtos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idCozinha",
                table: "Produtos");

            migrationBuilder.AddColumn<int>(
                name: "cozinhaidCozinha",
                table: "Produtos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_cozinhaidCozinha",
                table: "Produtos",
                column: "cozinhaidCozinha");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Cozinhas_cozinhaidCozinha",
                table: "Produtos",
                column: "cozinhaidCozinha",
                principalTable: "Cozinhas",
                principalColumn: "idCozinha",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
