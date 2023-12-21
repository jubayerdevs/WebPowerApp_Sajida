using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPowerApp.Migrations
{
    /// <inheritdoc />
    public partial class update_report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "workspaceId",
                table: "Reports",
                newName: "WorkspaceId");

            migrationBuilder.RenameColumn(
                name: "urlPowerBiServiceApiRoot",
                table: "Reports",
                newName: "UrlPowerBiServiceApiRoot");

            migrationBuilder.RenameColumn(
                name: "tenant",
                table: "Reports",
                newName: "Tenant");

            migrationBuilder.RenameColumn(
                name: "scope",
                table: "Reports",
                newName: "Scope");

            migrationBuilder.RenameColumn(
                name: "reportId",
                table: "Reports",
                newName: "ReportId");

            migrationBuilder.RenameColumn(
                name: "authorityUrl",
                table: "Reports",
                newName: "AuthorityUrl");

            migrationBuilder.RenameColumn(
                name: "authenticationType",
                table: "Reports",
                newName: "AuthenticationType");

            migrationBuilder.RenameColumn(
                name: "applicationSecret",
                table: "Reports",
                newName: "ApplicationSecret");

            migrationBuilder.RenameColumn(
                name: "applicationId",
                table: "Reports",
                newName: "ApplicationId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReportName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportName",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "WorkspaceId",
                table: "Reports",
                newName: "workspaceId");

            migrationBuilder.RenameColumn(
                name: "UrlPowerBiServiceApiRoot",
                table: "Reports",
                newName: "urlPowerBiServiceApiRoot");

            migrationBuilder.RenameColumn(
                name: "Tenant",
                table: "Reports",
                newName: "tenant");

            migrationBuilder.RenameColumn(
                name: "Scope",
                table: "Reports",
                newName: "scope");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Reports",
                newName: "reportId");

            migrationBuilder.RenameColumn(
                name: "AuthorityUrl",
                table: "Reports",
                newName: "authorityUrl");

            migrationBuilder.RenameColumn(
                name: "AuthenticationType",
                table: "Reports",
                newName: "authenticationType");

            migrationBuilder.RenameColumn(
                name: "ApplicationSecret",
                table: "Reports",
                newName: "applicationSecret");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Reports",
                newName: "applicationId");
        }
    }
}
