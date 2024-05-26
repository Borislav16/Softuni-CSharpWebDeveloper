using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class UniqueConstraintForPhoneNumberAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e568b895-deb2-4827-8725-54672089cb82", "AQAAAAEAACcQAAAAEO1MU8Fr/8BIOXqQbmgPJCyw7M8wiy5gnU6Qh8zR3JETm4uyAFt2bw4+E8NuJY+Xlg==", "fec0f635-f873-49e1-89da-4dcc93c8b2b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4cc8f2b5-2a7d-44d3-a4f5-9d6e33d43cda", "AQAAAAEAACcQAAAAEGF/MJm09XVpN/n/xqkiKIVpQ6En2uCPB0xhc2FBNtUv386+xz5yT5SNM7hGBUDkUQ==", "4949be26-5482-4bb8-af22-0e923bace2a6" });
        }
    }
}
