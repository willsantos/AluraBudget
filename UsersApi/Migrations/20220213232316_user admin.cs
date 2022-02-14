using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersApi.Migrations
{
    public partial class useradmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99999, "8512494a-3f9b-4327-bd09-a8db4dee6aec", "admins", "ADMINS" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 99999, 0, "9ab8b83b-900a-48de-a097-cbbdf626592d", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMINS", "AQAAAAEAACcQAAAAEAE/NJHry8SBsbtJKbsLc7uDGmK4AaN4XWM9cSV+CZQHeq+F4IZLB4I0CjMhAGFRaw==", null, false, "f73430c1-7806-49d0-90fb-f95edab18739", false, "admins" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 99999, 99999 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 99999, 99999 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999);
        }
    }
}
