using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Date", "EndTime", "StartTime", "Title" },
                values: new object[,]
                {
                    { new Guid("2d5d64d3-d7f7-4222-a5f1-69e92940cb95"), 40, new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7679), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7681), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7680), "Tytuł Wydarzenia 2" },
                    { new Guid("57fa9b48-99f2-43a7-9203-fc030f5210f2"), 50, new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7683), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7685), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7684), "Tytuł Wydarzenia 3" },
                    { new Guid("95a301ac-2a9c-4fb2-9157-3f274b8dd8b9"), 30, new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7628), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7675), new DateTime(2023, 12, 3, 6, 41, 55, 339, DateTimeKind.Local).AddTicks(7674), "Tytuł Wydarzenia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
    }
}
