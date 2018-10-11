using Microsoft.EntityFrameworkCore.Migrations;

namespace appmanager.Migrations
{
    public partial class UpdatedDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "Environment",
                table: "Servers",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminUrl",
                table: "Instances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Instances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "AdminUrl",
                table: "Instances");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Instances");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Servers",
                newName: "Environment");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Applications",
                nullable: true);
        }
    }
}
