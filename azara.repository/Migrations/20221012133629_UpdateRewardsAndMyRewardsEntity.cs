using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class UpdateRewardsAndMyRewardsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableRewards",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "RemainingRewards",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "UsedRewards",
                table: "Rewards");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "MyRewards",
                newName: "UsedRewards");

            migrationBuilder.AddColumn<bool>(
                name: "AvailableRewards",
                table: "MyRewards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MyRewards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "RemainingRewards",
                table: "MyRewards",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableRewards",
                table: "MyRewards");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MyRewards");

            migrationBuilder.DropColumn(
                name: "RemainingRewards",
                table: "MyRewards");

            migrationBuilder.RenameColumn(
                name: "UsedRewards",
                table: "MyRewards",
                newName: "IsUsed");

            migrationBuilder.AddColumn<string>(
                name: "AvailableRewards",
                table: "Rewards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RemainingRewards",
                table: "Rewards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsedRewards",
                table: "Rewards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
