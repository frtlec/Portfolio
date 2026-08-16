using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Porfolio.Services.Setting.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "settings");

            migrationBuilder.CreateTable(
                name: "AboutPages",
                schema: "settings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PortreFileName = table.Column<string>(type: "text", nullable: true),
                    Slogan = table.Column<string>(type: "text", nullable: true),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    Softwares = table.Column<string>(type: "jsonb", nullable: true),
                    Businesses = table.Column<string>(type: "jsonb", nullable: true),
                    Educations = table.Column<string>(type: "jsonb", nullable: true),
                    Certifacates = table.Column<string>(type: "jsonb", nullable: true),
                    Projects = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                schema: "settings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    LocalizationType = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutPages",
                schema: "settings");

            migrationBuilder.DropTable(
                name: "Localizations",
                schema: "settings");
        }
    }
}
