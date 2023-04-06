using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class ModifyCouponsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CouponsId",
                table: "MyRewards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyRewards_CouponsId",
                table: "MyRewards",
                column: "CouponsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyRewards_Coupons_CouponsId",
                table: "MyRewards",
                column: "CouponsId",
                principalTable: "Coupons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyRewards_Coupons_CouponsId",
                table: "MyRewards");

            migrationBuilder.DropIndex(
                name: "IX_MyRewards_CouponsId",
                table: "MyRewards");

            migrationBuilder.DropColumn(
                name: "CouponsId",
                table: "MyRewards");
        }
    }
}
