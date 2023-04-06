using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azara.repository.Migrations
{
    public partial class newDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyRewards");


            migrationBuilder.CreateTable(
                name: "MyReward",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableRewards = table.Column<bool>(type: "bit", nullable: true),
                    UsedRewards = table.Column<bool>(type: "bit", nullable: true),
                    RemainingRewards = table.Column<bool>(type: "bit", nullable: true),
                    RemainingRewards1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemainingRewards11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CouponsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PunchCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyReward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyReward_Coupons_CouponsId",
                        column: x => x.CouponsId,
                        principalTable: "Coupons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyReward_PunchCards_PunchCardId",
                        column: x => x.PunchCardId,
                        principalTable: "PunchCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyReward_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyReward_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyReward_CouponsId",
                table: "MyReward",
                column: "CouponsId");

            migrationBuilder.CreateIndex(
                name: "IX_MyReward_PunchCardId",
                table: "MyReward",
                column: "PunchCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MyReward_RewardId",
                table: "MyReward",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_MyReward_UserId",
                table: "MyReward",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyReward");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "POSInventory");

            migrationBuilder.CreateTable(
                name: "MyRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CouponsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PunchCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AvailableRewards = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemainingRewards = table.Column<bool>(type: "bit", nullable: true),
                    UsedRewards = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyRewards_Coupons_CouponsId",
                        column: x => x.CouponsId,
                        principalTable: "Coupons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyRewards_PunchCards_PunchCardId",
                        column: x => x.PunchCardId,
                        principalTable: "PunchCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyRewards_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyRewards_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyRewards_CouponsId",
                table: "MyRewards",
                column: "CouponsId");

            migrationBuilder.CreateIndex(
                name: "IX_MyRewards_PunchCardId",
                table: "MyRewards",
                column: "PunchCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MyRewards_RewardId",
                table: "MyRewards",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_MyRewards_UserId",
                table: "MyRewards",
                column: "UserId");
        }
    }
}
