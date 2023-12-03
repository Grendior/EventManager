using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("2d5d64d3-d7f7-4222-a5f1-69e92940cb95"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("57fa9b48-99f2-43a7-9203-fc030f5210f2"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("95a301ac-2a9c-4fb2-9157-3f274b8dd8b9"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Date", "EndTime", "StartTime", "Title" },
                values: new object[,]
                {
                    { new Guid("280871f9-d272-466c-8dff-de3fc131ea60"), 30, new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5427), new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5471), new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5470), "Tytuł Wydarzenia" },
                    { new Guid("61a47fab-79d4-426c-b777-fc90197da4b0"), 50, new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5479), new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5481), new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5480), "Tytuł Wydarzenia 3" },
                    { new Guid("e77ccb6f-8d43-410f-81db-476c5e4bec12"), 40, new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5475), new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5477), new DateTime(2023, 12, 3, 14, 28, 4, 65, DateTimeKind.Local).AddTicks(5476), "Tytuł Wydarzenia 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("280871f9-d272-466c-8dff-de3fc131ea60"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("61a47fab-79d4-426c-b777-fc90197da4b0"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e77ccb6f-8d43-410f-81db-476c5e4bec12"));

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Date", "EndTime", "StartTime", "Title" },
                values: new object[,]
                {
                    { new Guid("2d5d64d3-d7f7-4222-a5f1-69e92940cb95"), 40, new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7679), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7681), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7680), "Tytuł Wydarzenia 2" },
                    { new Guid("57fa9b48-99f2-43a7-9203-fc030f5210f2"), 50, new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7683), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7685), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7684), "Tytuł Wydarzenia 3" },
                    { new Guid("95a301ac-2a9c-4fb2-9157-3f274b8dd8b9"), 30, new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7628), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7675), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7674), "Tytuł Wydarzenia" }
                });
        }
    }
}
