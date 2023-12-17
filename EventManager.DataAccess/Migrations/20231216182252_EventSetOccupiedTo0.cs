using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EventSetOccupiedTo0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatorId", "Date", "EndTime", "Occupied", "StartTime", "Title" },
                values: new object[,]
                {
                    { "10b73d51-5de3-41fe-a59b-7f8ce78f47c2", 50, null, new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6555), new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6557), 0, new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6556), "Tytuł Wydarzenia 3" },
                    { "7d8fa6e8-2e33-40cc-81ff-d63f16f6cc5f", 30, null, new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6504), new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6546), 0, new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6545), "Tytuł Wydarzenia" },
                    { "80912791-4de4-4f5f-907c-193b9c110ef4", 40, null, new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6551), new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6553), 0, new DateTime(2023, 12, 16, 19, 22, 51, 832, DateTimeKind.Local).AddTicks(6552), "Tytuł Wydarzenia 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "10b73d51-5de3-41fe-a59b-7f8ce78f47c2");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "7d8fa6e8-2e33-40cc-81ff-d63f16f6cc5f");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "80912791-4de4-4f5f-907c-193b9c110ef4");

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
    }
}
