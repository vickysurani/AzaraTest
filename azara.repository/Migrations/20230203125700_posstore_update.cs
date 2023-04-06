using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class posstore_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSStores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnHandStore01 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore02 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore03 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore04 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore05 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore06 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore07 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore08 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore09 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore10 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore11 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore12 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore13 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore14 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore15 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore16 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore17 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore18 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore19 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnHandStore20 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSStores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSStores");
        }
    }
}
