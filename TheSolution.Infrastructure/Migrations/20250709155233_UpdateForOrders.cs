using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductID",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderID",
                table: "OrderProducts");

            migrationBuilder.AlterColumn<int>(
                name: "TotalAmount",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "OPID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderID1",
                table: "OrderProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderID_ProductID",
                table: "OrderProducts",
                columns: new[] { "OrderID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderID1",
                table: "OrderProducts",
                column: "OrderID1",
                unique: true,
                filter: "[OrderID1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderID1",
                table: "OrderProducts",
                column: "OrderID1",
                principalTable: "Orders",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductID",
                table: "OrderProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderID1",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductID",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderID_ProductID",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderID1",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "OPID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderID1",
                table: "OrderProducts");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderID",
                table: "OrderProducts",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductID",
                table: "OrderProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
