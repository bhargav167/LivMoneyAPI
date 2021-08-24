using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivMoneyAPI.Migrations
{
    public partial class AuthUserConfirm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AuthUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AuthUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirm",
                table: "AuthUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMobilConfirm",
                table: "AuthUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "AuthUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AuthUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AuthUsers");

            migrationBuilder.DropColumn(
                name: "IsEmailConfirm",
                table: "AuthUsers");

            migrationBuilder.DropColumn(
                name: "IsMobilConfirm",
                table: "AuthUsers");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "AuthUsers");
        }
    }
}
