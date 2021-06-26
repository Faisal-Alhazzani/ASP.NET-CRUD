using Microsoft.EntityFrameworkCore.Migrations;

namespace OrdersApplications.Migrations
{
    public partial class updateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Restaurant",
                table: "Cars",
                newName: "name");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Cars",
                newName: "Restaurant");
        }
    }
}
