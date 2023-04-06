using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class POSStoreDemo_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSCustomerDemo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AccountLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AmountPastDue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDiscPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerDiscType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultShipAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAcceptingChecks = table.Column<bool>(type: "bit", nullable: true),
                    IsNoShipToBilling = table.Column<bool>(type: "bit", nullable: true),
                    IsOkToEMail = table.Column<bool>(type: "bit", nullable: true),
                    IsRewardsMember = table.Column<bool>(type: "bit", nullable: true),
                    IsUsingChargeAccount = table.Column<bool>(type: "bit", nullable: true),
                    IsUsingWithQB = table.Column<bool>(type: "bit", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastSale = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceLevelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreExchangeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillAddressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillAddressCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillAddressPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillAddressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillAddressStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillAddressStreet2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldSalesLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldSerial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldSerial1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldSerial2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSCustomerDemo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSCustomerDemo");
        }
    }
}
