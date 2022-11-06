using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusalaGatewayProject.Migrations
{
    public partial class CreateAndSeeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateways",
                columns: table => new
                {
                    GatewayId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.GatewayId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    UID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusOfDevice = table.Column<int>(type: "int", nullable: false),
                    GatewayId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.UID);
                    table.ForeignKey(
                        name: "FK_Devices_Gateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateways",
                        principalColumn: "GatewayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gateways",
                columns: new[] { "GatewayId", "Address", "Name" },
                values: new object[,]
                {
                    { "1571C36C-7EBF-4B03-A4E9-81B24910B9A0", "127.0.0.1", "TestGateway#01" },
                    { "CAD72B56-2016-4291-B5F4-CFA2A3FED38C", "192.168.172.5", "TestGateway#02" },
                    { "E9BADFA9-51E2-4BA1-8FD9-583AC030E1B3", "255.255.255.128", "TestGateway#03" },
                    { "D3EEC6A0-E645-41F2-9ED8-93FF0F277D8E", "129.145.175.145", "TestGateway#04" },
                    { "695E5928-92AC-44A8-B122-1C5036BCE7C2", "197.245.127.247", "TestGateway#05" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { 1, new byte[] { 27, 216, 92, 75, 242, 3, 74, 191, 60, 239, 195, 18, 219, 243, 220, 18, 241, 214, 101, 73, 203, 62, 236, 3, 123, 225, 98, 226, 191, 215, 102, 78 }, new byte[] { 116, 135, 249, 15, 9, 49, 3, 246, 246, 103, 143, 77, 56, 27, 54, 165, 148, 110, 182, 49, 94, 232, 103, 59, 126, 102, 195, 26, 247, 164, 170, 171, 195, 4, 125, 3, 81, 66, 50, 139, 54, 175, 140, 7, 56, 195, 125, 66, 187, 38, 91, 142, 145, 144, 117, 57, 192, 48, 104, 192, 1, 60, 143, 66 }, "Admin" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "UID", "DateCreated", "GatewayId", "StatusOfDevice", "Vendor" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 14, 22, 4, 15, 8, DateTimeKind.Local).AddTicks(3417), "1571C36C-7EBF-4B03-A4E9-81B24910B9A0", 0, "Asus" },
                    { 15, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9376), "695E5928-92AC-44A8-B122-1C5036BCE7C2", 1, "AMD" },
                    { 13, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9370), "695E5928-92AC-44A8-B122-1C5036BCE7C2", 0, "AOC" },
                    { 9, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9357), "695E5928-92AC-44A8-B122-1C5036BCE7C2", 0, "Apple" },
                    { 8, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9353), "695E5928-92AC-44A8-B122-1C5036BCE7C2", 0, "Microsoft" },
                    { 7, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9350), "695E5928-92AC-44A8-B122-1C5036BCE7C2", 0, "Nvidia" },
                    { 3, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9334), "695E5928-92AC-44A8-B122-1C5036BCE7C2", 1, "Intel" },
                    { 12, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9367), "D3EEC6A0-E645-41F2-9ED8-93FF0F277D8E", 0, "Sony" },
                    { 11, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9364), "D3EEC6A0-E645-41F2-9ED8-93FF0F277D8E", 0, "Panasonic" },
                    { 19, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9389), "695E5928-92AC-44A8-B122-1C5036BCE7C2", 0, "Oracle" },
                    { 10, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9360), "D3EEC6A0-E645-41F2-9ED8-93FF0F277D8E", 1, "Apple" },
                    { 5, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9343), "E9BADFA9-51E2-4BA1-8FD9-583AC030E1B3", 0, "Cisco" },
                    { 2, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9287), "E9BADFA9-51E2-4BA1-8FD9-583AC030E1B3", 1, "Amazon" },
                    { 20, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9392), "CAD72B56-2016-4291-B5F4-CFA2A3FED38C", 0, "AMD" },
                    { 17, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9383), "CAD72B56-2016-4291-B5F4-CFA2A3FED38C", 0, "Google" },
                    { 4, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9339), "CAD72B56-2016-4291-B5F4-CFA2A3FED38C", 1, "Intel" },
                    { 18, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9386), "1571C36C-7EBF-4B03-A4E9-81B24910B9A0", 0, "Nvidia" },
                    { 16, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9379), "1571C36C-7EBF-4B03-A4E9-81B24910B9A0", 1, "Intel" },
                    { 14, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9373), "1571C36C-7EBF-4B03-A4E9-81B24910B9A0", 0, "Cooler Master" },
                    { 6, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9347), "D3EEC6A0-E645-41F2-9ED8-93FF0F277D8E", 0, "Amazon" },
                    { 21, new DateTime(2022, 10, 14, 22, 4, 15, 10, DateTimeKind.Local).AddTicks(9395), "695E5928-92AC-44A8-B122-1C5036BCE7C2", 1, "Nvidia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_GatewayId",
                table: "Devices",
                column: "GatewayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Gateways");
        }
    }
}
