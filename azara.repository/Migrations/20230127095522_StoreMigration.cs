using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class StoreMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore03",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore04",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore05",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore06",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore07",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore08",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore09",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore10",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore11",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore12",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore13",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore14",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore15",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore16",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore17",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore18",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore19",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHandStore20",
                table: "POSInventory",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnHandStore03",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore04",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore05",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore06",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore07",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore08",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore09",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore10",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore11",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore12",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore13",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore14",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore15",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore16",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore17",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore18",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore19",
                table: "POSInventory");

            migrationBuilder.DropColumn(
                name: "OnHandStore20",
                table: "POSInventory");
        }
    }
}
