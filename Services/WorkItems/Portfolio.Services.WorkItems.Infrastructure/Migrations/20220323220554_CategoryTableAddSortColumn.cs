using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Services.WorkItems.Infrastructure.Migrations
{
    public partial class CategoryTableAddSortColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Sort",
                schema: "works",
                table: "Categories",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                schema: "works",
                table: "Categories");
        }
    }
}
