using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apis.Migrations
{
    public partial class AddAddressForOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Ratings");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
