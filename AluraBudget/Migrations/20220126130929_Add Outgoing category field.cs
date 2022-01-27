using Microsoft.EntityFrameworkCore.Migrations;

namespace AluraBudget.Migrations
{
    public partial class AddOutgoingcategoryfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Outgoings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Outgoings");
        }
    }
}
