using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventAddImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatorId", "Date", "Description", "EndTime", "ImageUrl", "Occupied", "StartTime", "Title" },
                values: new object[,]
                {
                    { "02812184-4257-447f-9bb7-446e7d94db2e", 30, null, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8616), null, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8673), "", 0, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8672), "Tytuł Wydarzenia" },
                    { "a28f434e-42d1-4e20-9185-eddea9c0d24d", 40, null, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8679), null, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8682), "", 0, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8681), "Tytuł Wydarzenia 2" },
                    { "c52ad0e8-ca9d-4759-8f3e-e868d0c8d149", 50, null, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8684), null, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8686), "", 0, new DateTime(2024, 1, 22, 5, 44, 11, 420, DateTimeKind.Local).AddTicks(8685), "Tytuł Wydarzenia 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Events");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatorId", "Date", "Description", "EndTime", "Occupied", "StartTime", "Title" },
                values: new object[,]
                {
                    { "19500b2d-a29e-4a61-8708-3d5f13db367c", 40, null, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1733), null, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1735), 0, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1734), "Tytuł Wydarzenia 2" },
                    { "790c2361-b4ba-4adf-9882-12f5370d4363", 30, null, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1674), null, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1727), 0, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1725), "Tytuł Wydarzenia" },
                    { "c3f42cc2-6bc9-4439-a3a7-e98d2d28f332", 50, null, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1738), null, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1741), 0, new DateTime(2023, 12, 24, 18, 42, 35, 791, DateTimeKind.Local).AddTicks(1740), "Tytuł Wydarzenia 3" }
                });
        }
    }
}
