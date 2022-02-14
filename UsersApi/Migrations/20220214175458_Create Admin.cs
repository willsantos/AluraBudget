using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersApi.Migrations
{
    public partial class CreateAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "f6310ae9-7806-4271-83da-e75bd9519982");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99998, "95998d29-b524-4133-b9f7-d6eab8f06004", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "217b2b02-3850-4394-a12d-037affeaa513", "AQAAAAEAACcQAAAAEGNUAXs/7Q+EWoJl+rDI2BVIp1fdRBlo0yXEF9HP+SJjsYAtcse4cKQN7ILkkMLWRw==", "4bfa5fb7-6fa7-4286-b8e2-0d3b8104b6c9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "8512494a-3f9b-4327-bd09-a8db4dee6aec");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ab8b83b-900a-48de-a097-cbbdf626592d", "AQAAAAEAACcQAAAAEAE/NJHry8SBsbtJKbsLc7uDGmK4AaN4XWM9cSV+CZQHeq+F4IZLB4I0CjMhAGFRaw==", "f73430c1-7806-49d0-90fb-f95edab18739" });
        }
    }
}
