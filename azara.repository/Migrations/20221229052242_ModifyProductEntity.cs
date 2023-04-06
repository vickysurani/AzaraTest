using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class ModifyProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RewardsId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_RewardsId",
                table: "Product",
                column: "RewardsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Rewards_RewardsId",
                table: "Product",
                column: "RewardsId",
                principalTable: "Rewards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Rewards_RewardsId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_RewardsId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "RewardsId",
                table: "Product");
        }
    }
}
