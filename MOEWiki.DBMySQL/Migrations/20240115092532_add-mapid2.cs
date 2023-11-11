using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addmapid2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "MarkerGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MarkerGroups_MapId",
                table: "MarkerGroups",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_MapMarkers_MapId",
                table: "MapMarkers",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapMarkers_Maps_MapId",
                table: "MapMarkers",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerGroups_Maps_MapId",
                table: "MarkerGroups",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapMarkers_Maps_MapId",
                table: "MapMarkers");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkerGroups_Maps_MapId",
                table: "MarkerGroups");

            migrationBuilder.DropIndex(
                name: "IX_MarkerGroups_MapId",
                table: "MarkerGroups");

            migrationBuilder.DropIndex(
                name: "IX_MapMarkers_MapId",
                table: "MapMarkers");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "MarkerGroups");
        }
    }
}
