using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amalia.Data.Migrations
{
    public partial class NewCategoryColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Categories");
        }
    }
}
