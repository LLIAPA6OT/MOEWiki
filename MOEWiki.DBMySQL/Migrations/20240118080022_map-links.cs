using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class maplinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "MarkerGroups",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "MapZones",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "MapMarkers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "MarkerGroups");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "MapZones");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "MapMarkers");
        }
    }
}
