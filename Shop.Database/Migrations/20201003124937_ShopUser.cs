using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class ShopUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShopUserId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShopUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShopUserId",
                table: "Orders",
                column: "ShopUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShopUsers_ShopUserId",
                table: "Orders",
                column: "ShopUserId",
                principalTable: "ShopUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShopUsers_ShopUserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ShopUsers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShopUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShopUserId",
                table: "Orders");
        }
    }
}
