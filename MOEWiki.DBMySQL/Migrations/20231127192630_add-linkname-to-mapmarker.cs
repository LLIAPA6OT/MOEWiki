using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addlinknametomapmarker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChestLocationLink",
                table: "MapMarkers",
                newName: "LinkName");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "MapMarkers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "MapMarkers");

            migrationBuilder.RenameColumn(
                name: "LinkName",
                table: "MapMarkers",
                newName: "ChestLocationLink");
        }
    }
}
