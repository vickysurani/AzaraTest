using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class POSInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ALU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COGSAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentListID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncomeAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBelowReorder = table.Column<bool>(type: "bit", nullable: false),
                    IsEligibleForCommission = table.Column<bool>(type: "bit", nullable: false),
                    IsPrintingTags = table.Column<bool>(type: "bit", nullable: false),
                    IsUnorderable = table.Column<bool>(type: "bit", nullable: false),
                    ItemNumber = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastReceived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MarginPercent = table.Column<int>(type: "int", nullable: false),
                    MarkupPercent = table.Column<int>(type: "int", nullable: false),
                    MSRP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnHandStore01 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnHandStore02 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderByUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityOnCustomerOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityOnOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityOnPendingOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReorderPoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReorderPointStore01 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellByUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreExchangeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorListID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WebSKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebCategories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure1ALU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure1MSRP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure1NumberOfBaseUnits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure1Price1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure1Price2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure1Price3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure1Price4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure1Price5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure1UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure1UPC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure2ALU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure2MSRP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure2NumberOfBaseUnits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure2Price1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure2Price2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure2Price3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure2Price4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure2Price5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure2UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure2UPC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure3ALU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure3MSRP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure3NumberOfBaseUnits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure3Price1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure3Price2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure3Price3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure3Price4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure3Price5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure3UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure3UPC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo2ALU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo2OrderCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendorInfo2UPC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo2VendorListID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo3ALU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo3OrderCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendorInfo3UPC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo3VendorListID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo4ALU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo4OrderCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendorInfo4UPC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo4VendorListID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo5ALU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo5OrderCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendorInfo5UPC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorInfo5VendorListID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSInventory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSInventory");
        }
    }
}
