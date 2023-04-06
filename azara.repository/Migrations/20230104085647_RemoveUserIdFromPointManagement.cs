using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class RemoveUserIdFromPointManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointManagement_User_UserId",
                table: "PointManagement");

            migrationBuilder.DropIndex(
                name: "IX_PointManagement_UserId",
                table: "PointManagement");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PointManagement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PointManagement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointManagement_UserId",
                table: "PointManagement",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointManagement_User_UserId",
                table: "PointManagement",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
