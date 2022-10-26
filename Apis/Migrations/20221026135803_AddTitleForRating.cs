using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apis.Migrations
{
    public partial class AddTitleForRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Ratings");
        }
    }
}
