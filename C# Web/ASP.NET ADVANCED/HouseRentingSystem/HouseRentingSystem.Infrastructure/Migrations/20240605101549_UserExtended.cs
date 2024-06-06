using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class UserExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f98e6e9-9465-4382-96ff-347bd371f4df", "", "", "AQAAAAEAACcQAAAAEHqYdhrHP+CuGY3L5ANrY6ZZrmTlxR+kPmDJcnv+41Cyw46f1UYRlyXGlnE9BXS1tw==", "e176c4ab-df8b-4588-aab4-235167025b3c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ed595bb-c0ff-4437-bf86-72aedaa7c9e8", "", "", "AQAAAAEAACcQAAAAEMQcWYDPkrgE+GrWv+kCOT3m2rfQt7olPxcrGsqBXZGNjfQrnLs1yOk9cZfRC68pEA==", "aafb7f20-1d3a-44e6-b2e8-8a9c712efdd8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "714e460f-8c6c-4c15-ada6-b7fc7eef9195", "AQAAAAEAACcQAAAAEHgtNnSeSPk0LCMAj21PCyLt1A3n9vkBRvbMwl159Joj8wShs1QqE5vBHUdCP1GwyA==", "eac3a20b-99fd-4d3c-b593-2f1c8d0bc3bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79946f0f-9b67-4f96-960b-e74b2f09a79a", "AQAAAAEAACcQAAAAEGYhPvQzcDHE4Gj4CQR18xRreU1NXasobpoNaPK7e9UUbnYeNOAsSv+sgCHbpzrjww==", "75b189ab-4a1e-43ca-b244-140c8bc3cfb4" });
        }
    }
}
