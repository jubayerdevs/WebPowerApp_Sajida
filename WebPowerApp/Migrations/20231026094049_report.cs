using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPowerApp.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    authenticationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    workspaceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reportId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    authorityUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlPowerBiServiceApiRoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicationSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tenant = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
