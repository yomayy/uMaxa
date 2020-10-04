using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class ShopRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "ShopUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShopRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopUsers_RoleId",
                table: "ShopUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopUsers_ShopRoles_RoleId",
                table: "ShopUsers",
                column: "RoleId",
                principalTable: "ShopRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopUsers_ShopRoles_RoleId",
                table: "ShopUsers");

            migrationBuilder.DropTable(
                name: "ShopRoles");

            migrationBuilder.DropIndex(
                name: "IX_ShopUsers_RoleId",
                table: "ShopUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "ShopUsers");
        }
    }
}
