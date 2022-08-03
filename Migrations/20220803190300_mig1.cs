using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManSys.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_SellingCarts_SellingCartId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_SellingCartId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "SCartId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "SellingCartId",
                table: "CartProducts");

            migrationBuilder.CreateTable(
                name: "SCartProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCartProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SCartProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCartProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCartProducts_SellingCarts_CartId",
                        column: x => x.CartId,
                        principalTable: "SellingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SCartProducts_CartId",
                table: "SCartProducts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_SCartProducts_OrderId",
                table: "SCartProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SCartProducts_ProductId",
                table: "SCartProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SCartProducts");

            migrationBuilder.AddColumn<int>(
                name: "SCartId",
                table: "CartProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SellingCartId",
                table: "CartProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_SellingCartId",
                table: "CartProducts",
                column: "SellingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_SellingCarts_SellingCartId",
                table: "CartProducts",
                column: "SellingCartId",
                principalTable: "SellingCarts",
                principalColumn: "Id");
        }
    }
}
