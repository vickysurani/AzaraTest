using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class ModifyPunchcardEntites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PunchCardId",
                table: "MyRewards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyRewards_PunchCardId",
                table: "MyRewards",
                column: "PunchCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyRewards_PunchCards_PunchCardId",
                table: "MyRewards",
                column: "PunchCardId",
                principalTable: "PunchCards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyRewards_PunchCards_PunchCardId",
                table: "MyRewards");

            migrationBuilder.DropIndex(
                name: "IX_MyRewards_PunchCardId",
                table: "MyRewards");

            migrationBuilder.DropColumn(
                name: "PunchCardId",
                table: "MyRewards");
        }
    }
}
