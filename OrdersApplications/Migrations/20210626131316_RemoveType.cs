using Microsoft.EntityFrameworkCore.Migrations;

namespace OrdersApplications.Migrations
{
    public partial class RemoveType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
