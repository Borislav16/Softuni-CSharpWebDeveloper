using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class UserClaimsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "user:fullname", "Agent Agentov", "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 2, "user:fullname", "Guest Guestov", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 3, "user:fullname", "Great Admin", "bcb4f072-ecca-43c9-ab26-c060c6f364e4" }
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "924a79df-0bb9-4f74-921c-46462615aeef", "AQAAAAEAACcQAAAAEHkS4sDG6V0ofy65r3Hm1yxyz/NHioJ164Z51lNfOb2/1jXPRrTT0mKhcxs9D1lc2Q==", "db5f4b87-7931-45e2-aee6-7c6da6b0ef29" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7e2e74b-e0d0-46e6-80f4-75eaba0378bd", "AQAAAAEAACcQAAAAEE1hnoiAGb6hAjOhH34uFGRb22iaD0OYl8zWSETYnnvE4X0p1GaA3WUdK9Yepz6ptg==", "72474670-2a0a-47e4-8869-51a6c8faeea4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea2aa840-518d-4fb3-aeb5-4b5dbb2956d4", "AQAAAAEAACcQAAAAEJymr9dkVzLdabKIYssURjNbXUnM1LDNfPfqVU211WjJEgyCvZKA9lZiJGBoF3yaHw==", "9e019853-a510-435d-b988-7c2644420797" });
        }
    }
}
