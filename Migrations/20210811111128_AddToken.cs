using Microsoft.EntityFrameworkCore.Migrations;

namespace LivMoneyAPI.Migrations
{
    public partial class AddToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Otp",
                table: "AuthUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AuthUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Otp",
                table: "AuthUsers");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "AuthUsers");
        }
    }
}
