using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWorkDonorSite.Migrations
{
    public partial class AddCityAndStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "BloodTransfusionStations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTransfusionStations", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_BloodTransfusionStations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[] { 1, "Kiyv" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[] { 2, "Chernihiv" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[] { 3, "Lviv" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[] { 4, "Rivne" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[] { 5, "Zhytomyr" });

            migrationBuilder.InsertData(
                table: "BloodTransfusionStations",
                columns: new[] { "StationId", "Address", "CityId", "Name" },
                values: new object[] { 1, "Київ, вул. В. Чорновола, 28/1, новий корпус, Сектор Б, Центр служби крові", 1, "Центр служби крові Національної дитячої спеціалізованої лікарні ОХМАТДИТ" });

            migrationBuilder.InsertData(
                table: "BloodTransfusionStations",
                columns: new[] { "StationId", "Address", "CityId", "Name" },
                values: new object[] { 2, "Київ, вул. Максима Берлинського, 12", 1, "Київський міський центр крові" });

            migrationBuilder.InsertData(
                table: "BloodTransfusionStations",
                columns: new[] { "StationId", "Address", "CityId", "Name" },
                values: new object[] { 3, "Чернігів, вул. Пирогова, 13", 2, "Чернігівський обласний центр крові" });

            migrationBuilder.InsertData(
                table: "BloodTransfusionStations",
                columns: new[] { "StationId", "Address", "CityId", "Name" },
                values: new object[] { 4, "Корюківка, вул. Шевченка, 101", 2, "Корюківська Центральна районна лікарня" });

            migrationBuilder.InsertData(
                table: "BloodTransfusionStations",
                columns: new[] { "StationId", "Address", "CityId", "Name" },
                values: new object[] { 5, "Львів, вул. Пекарська, 65", 3, "Львівський обласний центр служби крові" });

            migrationBuilder.InsertData(
                table: "BloodTransfusionStations",
                columns: new[] { "StationId", "Address", "CityId", "Name" },
                values: new object[] { 6, "Рівне, вул. С. Бандери, 31", 4, "Рівненський обласний центр служби крові" });

            migrationBuilder.InsertData(
                table: "BloodTransfusionStations",
                columns: new[] { "StationId", "Address", "CityId", "Name" },
                values: new object[] { 7, "Здолбунів, вул. Степана Бандери, 1", 4, "Здолбунівська центральна районна лікарня №1, відділення трансфузіології" });

            migrationBuilder.InsertData(
                table: "BloodTransfusionStations",
                columns: new[] { "StationId", "Address", "CityId", "Name" },
                values: new object[] { 8, "Житомир, вул. Кибальчича, 16", 5, "Житомирський обласний центр крові" });

            migrationBuilder.CreateIndex(
                name: "IX_BloodTransfusionStations_CityId",
                table: "BloodTransfusionStations",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodTransfusionStations");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
