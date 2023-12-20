using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatorId", "Date", "Description", "EndTime", "Occupied", "StartTime", "Title" },
                values: new object[,]
                {
                    { "6375b2fc-7aeb-404c-907b-f30ec120ce30", 50, null, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7431), null, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7433), 0, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7432), "Tytuł Wydarzenia 3" },
                    { "832d7c65-0249-4361-b036-85ceca0b8c6b", 30, null, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7349), null, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7391), 0, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7390), "Tytuł Wydarzenia" },
                    { "a1d609fe-e211-41b6-a4d6-1f58d8e0ae10", 40, null, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7396), null, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7398), 0, new DateTime(2023, 12, 20, 5, 33, 3, 86, DateTimeKind.Local).AddTicks(7397), "Tytuł Wydarzenia 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "6375b2fc-7aeb-404c-907b-f30ec120ce30");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "832d7c65-0249-4361-b036-85ceca0b8c6b");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "a1d609fe-e211-41b6-a4d6-1f58d8e0ae10");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

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
    }
}
