using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventAddOccupatiedAndCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "17780396-2f0b-4527-948c-0f1638f66a50");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "894e9a41-d848-4027-bcf5-074a01a6bbf0");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "e074165e-221e-425d-9f6a-dc46e368c81a");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Occupied",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatorId", "Date", "EndTime", "Occupied", "StartTime", "Title" },
                values: new object[,]
                {
                    { "f1b54f99-ec5c-48ae-a262-13e857582ff8", 50, null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(560), new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(562), null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(561), "Tytuł Wydarzenia 3" },
                    { "f4ebd3e6-5491-4816-bafd-ef313ed5a967", 40, null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(555), new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(557), null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(556), "Tytuł Wydarzenia 2" },
                    { "f647de77-2910-4345-a77c-4645c8b0adb1", 30, null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(508), new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(552), null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(551), "Tytuł Wydarzenia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CreatorId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "f1b54f99-ec5c-48ae-a262-13e857582ff8");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "f4ebd3e6-5491-4816-bafd-ef313ed5a967");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "f647de77-2910-4345-a77c-4645c8b0adb1");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Occupied",
                table: "Events");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Date", "EndTime", "StartTime", "Title" },
                values: new object[,]
                {
                    { "17780396-2f0b-4527-948c-0f1638f66a50", 40, new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5259), new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5261), new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5260), "Tytuł Wydarzenia 2" },
                    { "894e9a41-d848-4027-bcf5-074a01a6bbf0", 50, new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5263), new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5265), new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5264), "Tytuł Wydarzenia 3" },
                    { "e074165e-221e-425d-9f6a-dc46e368c81a", 30, new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5209), new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5253), new DateTime(2023, 12, 16, 11, 0, 49, 9, DateTimeKind.Local).AddTicks(5252), "Tytuł Wydarzenia" }
                });
        }
    }
}
