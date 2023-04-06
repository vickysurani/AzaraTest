using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class AddedTeamIdFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Admin",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_TeamId",
                table: "Admin",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Team_TeamId",
                table: "Admin",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Team_TeamId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_TeamId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Admin");
        }
    }
}
