using Microsoft.EntityFrameworkCore.Migrations;

namespace LivMoneyAPI.Migrations
{
    public partial class ImgUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AuthUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AuthUsers");
        }
    }
}
