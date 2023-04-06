using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class ChangeAdminEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Team_TeamId",
                table: "Admin");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Admin",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Admin_TeamId",
                table: "Admin",
                newName: "IX_Admin_RoleId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentRevisionNumber",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_MasterRoles_RoleId",
                table: "Admin",
                column: "RoleId",
                principalTable: "MasterRoles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_MasterRoles_RoleId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "CurrentRevisionNumber",
                table: "Admin");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Admin",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Admin_RoleId",
                table: "Admin",
                newName: "IX_Admin_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Team_TeamId",
                table: "Admin",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id");
        }
    }
}
