using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookingIsPending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "Bookings",
                newName: "IsAccepted");

            migrationBuilder.AddColumn<bool>(
                name: "IsPending",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPending",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "IsAccepted",
                table: "Bookings",
                newName: "IsConfirmed");
        }
    }
}
