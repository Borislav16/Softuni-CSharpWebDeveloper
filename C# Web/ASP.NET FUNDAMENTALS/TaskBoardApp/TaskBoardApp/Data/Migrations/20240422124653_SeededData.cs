using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class SeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3", 0, "24359b10-a6e9-4b8f-ad6a-4cdf3ebf61ad", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEObcfXFL91XEuEysXXBEfD/BYpS++Jpw5z/0sx1AgZZmFFClwLStAp18/vg1G+2TEw==", null, false, "c4053c91-50b1-45e0-9371-5c5297d99500", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 22, 15, 46, 53, 355, DateTimeKind.Local).AddTicks(7259), "string string string string strring string string string", "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3", "Imrpove CSS styles" },
                    { 2, 1, new DateTime(2023, 4, 22, 15, 46, 53, 355, DateTimeKind.Local).AddTicks(7312), "text string string string text string text text text string text", "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3", "Android Client App" },
                    { 3, 2, new DateTime(2024, 3, 22, 15, 46, 53, 355, DateTimeKind.Local).AddTicks(7318), "Text string text string string string string text", "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 9, 1, 15, 46, 53, 355, DateTimeKind.Local).AddTicks(7320), "String Text text String , more string, text", "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c8b7adf8-a97d-462e-9ef6-f778fa43cfc3");
        }
    }
}
