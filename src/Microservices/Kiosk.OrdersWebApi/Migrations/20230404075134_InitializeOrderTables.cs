using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kiosk.OrdersWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitializeOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Delivery_Type = table.Column<int>(type: "integer", nullable: false),
                    Order_Status = table.Column<int>(type: "integer", nullable: false),
                    Order_TotalPrice = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInOrder",
                columns: table => new
                {
                    ProductInOrder_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductInOrder_Name = table.Column<string>(type: "text", nullable: false),
                    ProductInOrder_Code = table.Column<string>(type: "text", nullable: false),
                    ProductInOrder_Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Order_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInOrder", x => x.ProductInOrder_Id);
                    table.ForeignKey(
                        name: "FK_ProductsInOrder_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInOrder_Order_Id",
                table: "ProductsInOrder",
                column: "Order_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsInOrder");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
