using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Services.WorkItems.Infrastructure.Migrations
{
    public partial class CategoryTableAddColumnIsShowMainPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShowMainPage",
                schema: "works",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShowMainPage",
                schema: "works",
                table: "Categories");
        }
    }
}
