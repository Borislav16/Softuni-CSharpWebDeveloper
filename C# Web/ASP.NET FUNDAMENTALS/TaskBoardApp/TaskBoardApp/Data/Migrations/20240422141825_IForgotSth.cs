using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class IForgotSth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c13e2094-ad78-4707-9207-4cf84f327865", 0, "98aa7ac3-c41e-4650-a83f-4754e113d561", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAENG9PCSZO4oLCogk76VPMgNklJn3YwEhPGd7h+mgIKI9gc9prHLv06CzO46YUc1YAA==", null, false, "361f7ad7-b4c3-4b35-9041-e41d8a658d60", false, "test@softuni.bg" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2024, 1, 22, 17, 18, 24, 904, DateTimeKind.Local).AddTicks(1489), "c13e2094-ad78-4707-9207-4cf84f327865" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 4, 22, 17, 18, 24, 904, DateTimeKind.Local).AddTicks(1526), "c13e2094-ad78-4707-9207-4cf84f327865" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2024, 3, 22, 17, 18, 24, 904, DateTimeKind.Local).AddTicks(1529), "c13e2094-ad78-4707-9207-4cf84f327865" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 9, 1, 17, 18, 24, 904, DateTimeKind.Local).AddTicks(1532), "c13e2094-ad78-4707-9207-4cf84f327865" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c13e2094-ad78-4707-9207-4cf84f327865");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3", 0, "24359b10-a6e9-4b8f-ad6a-4cdf3ebf61ad", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEObcfXFL91XEuEysXXBEfD/BYpS++Jpw5z/0sx1AgZZmFFClwLStAp18/vg1G+2TEw==", null, false, "c4053c91-50b1-45e0-9371-5c5297d99500", false, "test@softuni.bg" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2024, 1, 22, 15, 46, 53, 355, DateTimeKind.Local).AddTicks(7259), "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 4, 22, 15, 46, 53, 355, DateTimeKind.Local).AddTicks(7312), "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2024, 3, 22, 15, 46, 53, 355, DateTimeKind.Local).AddTicks(7318), "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 9, 1, 15, 46, 53, 355, DateTimeKind.Local).AddTicks(7320), "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
