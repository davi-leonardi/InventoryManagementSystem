using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManSys.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderVMId",
                table: "CartProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderVM", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_OrderId",
                table: "CartProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_OrderVMId",
                table: "CartProducts",
                column: "OrderVMId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Orders_OrderId",
                table: "CartProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_OrderVM_OrderVMId",
                table: "CartProducts",
                column: "OrderVMId",
                principalTable: "OrderVM",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Orders_OrderId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_OrderVM_OrderVMId",
                table: "CartProducts");

            migrationBuilder.DropTable(
                name: "OrderVM");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_OrderId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_OrderVMId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "OrderVMId",
                table: "CartProducts");
        }
    }
}
