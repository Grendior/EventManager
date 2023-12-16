using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventParticipantModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "63723907-2db2-457a-b1d8-15ef9cbd99c1");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "ec88f836-5c57-40cd-a5f6-b202b1b28194");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "f3554462-2d33-406d-851e-429a5bc4111e");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "EventParticipants");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "EventParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Date", "EndTime", "StartTime", "Title" },
                values: new object[,]
                {
                    { "63723907-2db2-457a-b1d8-15ef9cbd99c1", 40, new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3843), new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3846), new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3845), "Tytuł Wydarzenia 2" },
                    { "ec88f836-5c57-40cd-a5f6-b202b1b28194", 50, new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3848), new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3850), new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3849), "Tytuł Wydarzenia 3" },
                    { "f3554462-2d33-406d-851e-429a5bc4111e", 30, new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3800), new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3839), new DateTime(2023, 12, 6, 7, 4, 52, 676, DateTimeKind.Local).AddTicks(3838), "Tytuł Wydarzenia" }
                });
        }
    }
}
