using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationOutboxMessages : Migration
    {
        /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.CreateTable(
                    name: "NotificationOutboxMessages",
                    columns: table => new
                    {
                        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Error = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        LastTriedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_NotificationOutboxMessages", x => x.Id);
                    });
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DropTable(
                    name: "NotificationOutboxMessages");
            }
        }
    }

