using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class RenterUserFKAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_UserId",
                table: "Agents");

            migrationBuilder.AlterColumn<string>(
                name: "RenterId",
                table: "Houses",
                type: "nvarchar(450)",
                nullable: true,
                comment: "User id of the renterer",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "User id of the renterer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fcedfc6-08ad-4c2c-9c23-34eac9731518", "AQAAAAEAACcQAAAAEI4K2LnoYPCColao3RZiobxzeAMiMkqTZ1bBoxFKs1J7Qtudyj/5g1r3tAlYoCkeoA==", "7d2c382e-588f-4725-b109-c8a6129b8fba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b9e4f2b-e30e-4456-b028-7b84fbb5a5e9", "AQAAAAEAACcQAAAAEFKkmxeuI5pvoqU3CzEXahJ4hpO2XxrXQ/9H3hNAR83RgL+IW4AQxR60UcxTL3GHBA==", "761ba4da-0909-4892-8589-eb542ed56afe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4a383fc-2efa-48f0-a646-6654442d5bb3", "AQAAAAEAACcQAAAAEB7KkHTxAIIjZoTMhLI+UiBxYyAtE0dFU543s6dME2G8eWmF3LORtk5CPjDapswUkg==", "07179db8-edf7-4555-8593-8041ea43e0aa" });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_RenterId",
                table: "Houses",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserId",
                table: "Agents",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_RenterId",
                table: "Houses",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_RenterId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_RenterId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Agents_UserId",
                table: "Agents");

            migrationBuilder.AlterColumn<string>(
                name: "RenterId",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true,
                comment: "User id of the renterer",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true,
                oldComment: "User id of the renterer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24e39fdb-d033-4b9f-b10d-6dbe8000cf2a", "AQAAAAEAACcQAAAAEGOLhSm6pns5ZENp3vY1ojgSUZKOpSMj8ZvB7YQiGM62LU+xwlYStsTH9zeb+mQoQw==", "c601ca9c-f0bb-4259-8941-1847a74628b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76c0378e-7abb-447e-80c2-bb1051cb9dbf", "AQAAAAEAACcQAAAAEPq7FLjkyWF1Sk9MLoJRY5rjMycEoY8gwEap665qC57XWB8kDKmhYtCJ6kcb2o4YwQ==", "352348a9-770a-4d7b-8278-d29a65812d59" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a3dee70-b973-4d72-992e-131c05422248", "AQAAAAEAACcQAAAAEGHYc3WjtYSXjqzF3yuKPKOyEhNd7zUNpx9sCB1zxo3kdn0iHGPweezHm5DOT5Hacg==", "55a42486-e88d-4ddb-87a1-324349cb77c9" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserId",
                table: "Agents",
                column: "UserId");
        }
    }
}
