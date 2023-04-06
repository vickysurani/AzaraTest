using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class Updated_POSDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "POSDepartments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "POSDepartments");
        }
    }
}
