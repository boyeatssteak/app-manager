using Microsoft.EntityFrameworkCore.Migrations;

namespace appmanager.Migrations
{
    public partial class RevisedModelsAddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstanceServer_Instances_InstanceId",
                table: "InstanceServer");

            migrationBuilder.DropForeignKey(
                name: "FK_InstanceServer_Servers_ServerId",
                table: "InstanceServer");

            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_Vendors_VendorId1",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Platforms_VendorId1",
                table: "Platforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstanceServer",
                table: "InstanceServer");

            migrationBuilder.DropColumn(
                name: "VendorId1",
                table: "Platforms");

            migrationBuilder.DropColumn(
                name: "AdminUrl",
                table: "Instances");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Applications");

            migrationBuilder.RenameTable(
                name: "InstanceServer",
                newName: "InstanceServers");

            migrationBuilder.RenameIndex(
                name: "IX_InstanceServer_ServerId",
                table: "InstanceServers",
                newName: "IX_InstanceServers_ServerId");

            migrationBuilder.RenameIndex(
                name: "IX_InstanceServer_InstanceId",
                table: "InstanceServers",
                newName: "IX_InstanceServers_InstanceId");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "SecureAreas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Platforms",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstanceServers",
                table: "InstanceServers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_VendorId",
                table: "Platforms",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PlatformId",
                table: "Applications",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Platforms_PlatformId",
                table: "Applications",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceServers_Instances_InstanceId",
                table: "InstanceServers",
                column: "InstanceId",
                principalTable: "Instances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceServers_Servers_ServerId",
                table: "InstanceServers",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_Vendors_VendorId",
                table: "Platforms",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Platforms_PlatformId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_UserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_InstanceServers_Instances_InstanceId",
                table: "InstanceServers");

            migrationBuilder.DropForeignKey(
                name: "FK_InstanceServers_Servers_ServerId",
                table: "InstanceServers");

            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_Vendors_VendorId",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Platforms_VendorId",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PlatformId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_UserId",
                table: "Applications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstanceServers",
                table: "InstanceServers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Applications");

            migrationBuilder.RenameTable(
                name: "InstanceServers",
                newName: "InstanceServer");

            migrationBuilder.RenameIndex(
                name: "IX_InstanceServers_ServerId",
                table: "InstanceServer",
                newName: "IX_InstanceServer_ServerId");

            migrationBuilder.RenameIndex(
                name: "IX_InstanceServers_InstanceId",
                table: "InstanceServer",
                newName: "IX_InstanceServer_InstanceId");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "SecureAreas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "VendorId",
                table: "Platforms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "VendorId1",
                table: "Platforms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminUrl",
                table: "Instances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstanceServer",
                table: "InstanceServer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_VendorId1",
                table: "Platforms",
                column: "VendorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceServer_Instances_InstanceId",
                table: "InstanceServer",
                column: "InstanceId",
                principalTable: "Instances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceServer_Servers_ServerId",
                table: "InstanceServer",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_Vendors_VendorId1",
                table: "Platforms",
                column: "VendorId1",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
