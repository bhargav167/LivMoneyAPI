using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivMoneyAPI.Migrations
{
    public partial class PasswordHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "AuthUsers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "AuthUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AuthUsers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AuthUsers");
        }
    }
}
