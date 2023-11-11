using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addrowinperkandresearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConphessionPoint",
                table: "Perks",
                newName: "Row");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Researches",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Researches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComprehensionPoint",
                table: "Perks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Perks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Researches");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Researches");

            migrationBuilder.DropColumn(
                name: "ComprehensionPoint",
                table: "Perks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Perks");

            migrationBuilder.RenameColumn(
                name: "Row",
                table: "Perks",
                newName: "ConphessionPoint");
        }
    }
}
