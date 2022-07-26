using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManSys.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Orders_OrderId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_OrderId",
                table: "CartProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
