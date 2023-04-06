using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class removeteamidfromadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Team_TeamId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_TeamId",
                table: "Admin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
