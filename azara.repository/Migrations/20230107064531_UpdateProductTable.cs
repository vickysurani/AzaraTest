using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class UpdateProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Stores_StoreId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_StoreId",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_StoreId",
                table: "Product",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Stores_StoreId",
                table: "Product",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }
    }
}
