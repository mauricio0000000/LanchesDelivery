using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class nivelAcesso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nivelAcesso",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nivelAcesso",
                table: "AspNetUsers");
        }
    }
}
