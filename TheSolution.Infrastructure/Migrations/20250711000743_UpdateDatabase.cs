using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderProducts",
                newName: "TotalAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "OrderProducts",
                newName: "Quantity");
        }
    }
}
