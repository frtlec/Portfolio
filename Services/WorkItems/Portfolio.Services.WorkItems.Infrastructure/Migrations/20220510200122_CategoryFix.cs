using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Services.WorkItems.Infrastructure.Migrations
{
    public partial class CategoryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Categories_CategoryId1",
                schema: "works",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_CategoryId1",
                schema: "works",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                schema: "works",
                table: "Works");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                schema: "works",
                table: "Works",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.CreateIndex(
                name: "IX_Works_CategoryId",
                schema: "works",
                table: "Works",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Categories_CategoryId",
                schema: "works",
                table: "Works",
                column: "CategoryId",
                principalSchema: "works",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Categories_CategoryId",
                schema: "works",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_CategoryId",
                schema: "works",
                table: "Works");

            migrationBuilder.AlterColumn<short>(
                name: "CategoryId",
                schema: "works",
                table: "Works",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                schema: "works",
                table: "Works",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_CategoryId1",
                schema: "works",
                table: "Works",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Categories_CategoryId1",
                schema: "works",
                table: "Works",
                column: "CategoryId1",
                principalSchema: "works",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
