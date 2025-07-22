using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingWalletAmountToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "WalletAmount",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalletAmount",
                table: "Users");
        }
    }
}
