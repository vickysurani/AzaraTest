using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class ModifyPunchCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailablePunchCardPromo",
                table: "PunchCards");

            migrationBuilder.RenameColumn(
                name: "Promocode",
                table: "PunchCards",
                newName: "PromoCode");

            migrationBuilder.RenameColumn(
                name: "UsedPunchCardPromo",
                table: "PunchCards",
                newName: "PunchCardPromos");

            migrationBuilder.RenameColumn(
                name: "RemainingPunchCardPromo",
                table: "PunchCards",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PromoCode",
                table: "PunchCards",
                newName: "Promocode");

            migrationBuilder.RenameColumn(
                name: "PunchCardPromos",
                table: "PunchCards",
                newName: "UsedPunchCardPromo");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "PunchCards",
                newName: "RemainingPunchCardPromo");

            migrationBuilder.AddColumn<string>(
                name: "AvailablePunchCardPromo",
                table: "PunchCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
