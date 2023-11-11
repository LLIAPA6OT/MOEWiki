using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addmarkergroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequiredItemName",
                table: "Researches",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HTMLValue",
                table: "ItemPropertiesWithNonActual",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HTMLValue",
                table: "ItemProperties",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredItemName",
                table: "Researches");

            migrationBuilder.DropColumn(
                name: "HTMLValue",
                table: "ItemPropertiesWithNonActual");

            migrationBuilder.DropColumn(
                name: "HTMLValue",
                table: "ItemProperties");
        }
    }
}
