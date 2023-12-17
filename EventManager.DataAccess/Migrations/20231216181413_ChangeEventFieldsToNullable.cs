using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEventFieldsToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatorId", "Date", "EndTime", "Occupied", "StartTime", "Title" },
                values: new object[,]
                {
                    { "40ce9205-d8d9-47c2-b261-37a1b323afe6", 30, null, new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4365), new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4408), null, new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4407), "Tytuł Wydarzenia" },
                    { "844044c8-bbd8-480b-89ed-dcb151e42a61", 50, null, new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4417), new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4419), null, new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4418), "Tytuł Wydarzenia 3" },
                    { "acc5df0c-3abc-44ce-ab7e-ac6785f87d83", 40, null, new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4413), new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4415), null, new DateTime(2023, 12, 16, 19, 14, 12, 742, DateTimeKind.Local).AddTicks(4414), "Tytuł Wydarzenia 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "40ce9205-d8d9-47c2-b261-37a1b323afe6");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "844044c8-bbd8-480b-89ed-dcb151e42a61");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "acc5df0c-3abc-44ce-ab7e-ac6785f87d83");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatorId", "Date", "EndTime", "Occupied", "StartTime", "Title" },
                values: new object[,]
                {
                    { "f1b54f99-ec5c-48ae-a262-13e857582ff8", 50, null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(560), new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(562), null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(561), "Tytuł Wydarzenia 3" },
                    { "f4ebd3e6-5491-4816-bafd-ef313ed5a967", 40, null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(555), new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(557), null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(556), "Tytuł Wydarzenia 2" },
                    { "f647de77-2910-4345-a77c-4645c8b0adb1", 30, null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(508), new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(552), null, new DateTime(2023, 12, 16, 18, 38, 32, 981, DateTimeKind.Local).AddTicks(551), "Tytuł Wydarzenia" }
                });
        }
    }
}
