using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWorkDonorSite.Migrations
{
    public partial class AddDonorAndRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodDonors",
                columns: table => new
                {
                    BloodDonorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    DonorName = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    BloodType = table.Column<string>(type: "TEXT", nullable: true),
                    RhesusBlood = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonors", x => x.BloodDonorId);
                    table.ForeignKey(
                        name: "FK_BloodDonors_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodRecipients",
                columns: table => new
                {
                    BloodRecipientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipientName = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    BloodType = table.Column<string>(type: "TEXT", nullable: true),
                    RhesusBlood = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodRecipients", x => x.BloodRecipientId);
                    table.ForeignKey(
                        name: "FK_BloodRecipients_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonors_CityId",
                table: "BloodDonors",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodRecipients_CityId",
                table: "BloodRecipients",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodDonors");

            migrationBuilder.DropTable(
                name: "BloodRecipients");
        }
    }
}
