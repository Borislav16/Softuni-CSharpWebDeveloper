using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class AdminAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "242e3d59-977a-4d83-9176-e32e3b603b68", "Guest", "Guestov", "AQAAAAEAACcQAAAAEKrIUx6BRDnz8DMkmat9Eu3M05bk+Q1QeJH73P+0sr93Oxz5rLI/VnzxD1nTiwLXzg==", "04285949-4daa-4bd6-abf5-a519f1d16e64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fbb1940-9ce0-4272-aaae-5db82f6d5a87", "Agent", "Agentov", "AQAAAAEAACcQAAAAEO0QgjZca9FAEEe2XvZtaOntxGT8DxIFYMkaTxZFHcFAnN8K4llYeotpuryr5n9uSA==", "033f02aa-f9a9-4d72-81b4-f0d607580925" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bcb4f072-ecca-43c9-ab26-c060c6f364e4", 0, "b75605f4-fc1a-4da8-b5f2-92f0dc87aa57", "admin@mail.com", false, "Great", "Admin", false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEB2ZzkhuKGK9rWzIejuuerBd3HHtszIofzUGllZRzSfpKZAhuovyhyhCpVn5juCmeg==", null, false, "60d7d26d-39ba-42c4-867d-9d2e4aae5eba", false, "admin@mail.com" });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 3, "+359888888887", "bcb4f072-ecca-43c9-ab26-c060c6f364e4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4");

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
    }
}
