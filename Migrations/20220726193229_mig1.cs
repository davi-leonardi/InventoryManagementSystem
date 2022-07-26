using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManSys.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ShoppingCarts_cartId",
                table: "CartProducts");

            migrationBuilder.RenameColumn(
                name: "cartId",
                table: "CartProducts",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_cartId",
                table: "CartProducts",
                newName: "IX_CartProducts_CartId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CartProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_OrderId",
                table: "CartProducts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Orders_OrderId",
                table: "CartProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ShoppingCarts_CartId",
                table: "CartProducts",
                column: "CartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Orders_OrderId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ShoppingCarts_CartId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_OrderId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CartProducts");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartProducts",
                newName: "cartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_CartId",
                table: "CartProducts",
                newName: "IX_CartProducts_cartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ShoppingCarts_cartId",
                table: "CartProducts",
                column: "cartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
