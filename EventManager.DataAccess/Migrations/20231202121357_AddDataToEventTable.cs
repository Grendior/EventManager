using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToEventTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Date", "EndTime", "StartTime", "Title" },
                values: new object[,]
                {
                    { new Guid("8ae4fd63-5f1c-47f7-9f07-e053e28ead71"), 40, new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9161), new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9166), new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9164), "Tytuł Wydarzenia 2" },
                    { new Guid("a62b2f54-ade8-4a5c-8df0-0941608085f5"), 30, new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9098), new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9155), new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9153), "Tytuł Wydarzenia" },
                    { new Guid("eb97e843-3985-4341-ba35-0bcf5eda5176"), 50, new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9171), new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9177), new DateTime(2023, 12, 2, 13, 13, 56, 856, DateTimeKind.Local).AddTicks(9174), "Tytuł Wydarzenia 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("8ae4fd63-5f1c-47f7-9f07-e053e28ead71"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("a62b2f54-ade8-4a5c-8df0-0941608085f5"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("eb97e843-3985-4341-ba35-0bcf5eda5176"));
        }
    }
}
