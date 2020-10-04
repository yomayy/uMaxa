using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class builder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShopRoles",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[] { new Guid("858ced3d-07e3-4fa7-a69e-1ca69955fd10"), new DateTime(2020, 10, 3, 13, 12, 0, 869, DateTimeKind.Utc), new DateTime(2020, 10, 3, 13, 12, 0, 869, DateTimeKind.Utc), "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShopRoles",
                keyColumn: "Id",
                keyValue: new Guid("858ced3d-07e3-4fa7-a69e-1ca69955fd10"));
        }
    }
}
