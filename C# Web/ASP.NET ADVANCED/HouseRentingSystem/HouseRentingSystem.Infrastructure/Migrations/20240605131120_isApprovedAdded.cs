using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class isApprovedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Is house approved by admin");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 3,
                column: "PhoneNumber",
                value: "+359888888887");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Houses");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 3,
                column: "PhoneNumber",
                value: "+359123456789");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "242e3d59-977a-4d83-9176-e32e3b603b68", "AQAAAAEAACcQAAAAEKrIUx6BRDnz8DMkmat9Eu3M05bk+Q1QeJH73P+0sr93Oxz5rLI/VnzxD1nTiwLXzg==", "04285949-4daa-4bd6-abf5-a519f1d16e64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b75605f4-fc1a-4da8-b5f2-92f0dc87aa57", "AQAAAAEAACcQAAAAEB2ZzkhuKGK9rWzIejuuerBd3HHtszIofzUGllZRzSfpKZAhuovyhyhCpVn5juCmeg==", "60d7d26d-39ba-42c4-867d-9d2e4aae5eba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fbb1940-9ce0-4272-aaae-5db82f6d5a87", "AQAAAAEAACcQAAAAEO0QgjZca9FAEEe2XvZtaOntxGT8DxIFYMkaTxZFHcFAnN8K4llYeotpuryr5n9uSA==", "033f02aa-f9a9-4d72-81b4-f0d607580925" });
        }
    }
}
