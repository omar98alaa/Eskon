using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImagePropertyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Cities_Name_CountryId",
            //    table: "Cities",
            //    columns: new[] { "Name", "CountryId" },
            //    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_Cities_Name_CountryId",
            //    table: "Cities");

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
