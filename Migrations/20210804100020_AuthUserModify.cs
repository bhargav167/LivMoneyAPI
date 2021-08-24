using Microsoft.EntityFrameworkCore.Migrations;

namespace LivMoneyAPI.Migrations
{
    public partial class AuthUserModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AuthUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AuthUsers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AuthUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AuthUsers");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AuthUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AuthUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
