using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "02812184-4257-447f-9bb7-446e7d94db2e");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "a28f434e-42d1-4e20-9185-eddea9c0d24d");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "c52ad0e8-ca9d-4759-8f3e-e868d0c8d149");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatorId", "Date", "Description", "EndTime", "ImageUrl", "Occupied", "StartTime", "Title" },
                values: new object[] { "68c1e967-7b1a-4450-b6e4-f47356d4dd92", 12, null, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "<h1>Zajęcia Bokserskie w Słupsku</h1>\n<p>Witaj na stronie poświęconej naszym zajęciom bokserskim w Słupsku! Jesteśmy zespołem pasjonat&oacute;w, kt&oacute;rzy razem trenują i rozwijają swoje umiejętności bokserskie.</p>\n<h2>Jak Zacząć?</h2>\n<p>Jeśli jesteś zainteresowany/a dołączeniem do naszych zajęć, zapisz się do wydarzenia lub przyjdź na jedno z trening&oacute;w, aby dowiedzieć się więcej.</p>\n<p>Zapraszamy do wsp&oacute;lnego rozwijania umiejętności bokserskich i czerpania radości z trening&oacute;w!</p>", new DateTime(2024, 2, 4, 19, 14, 57, 881, DateTimeKind.Local).AddTicks(6435), "\\images\\event\\050665e9-bb54-4c15-98fc-482b95af60c9.jpg", 0, new DateTime(2024, 2, 4, 19, 14, 57, 881, DateTimeKind.Local).AddTicks(6364), "Zajęcia boks" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: "68c1e967-7b1a-4450-b6e4-f47356d4dd92");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
