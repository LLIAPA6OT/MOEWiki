using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addmarkergroup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarkerGroupId",
                table: "MapMarkers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MarkerGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkerGroups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MapMarkerToMarkerGroupRels",
                columns: table => new
                {
                    MarkerGroupId = table.Column<int>(type: "int", nullable: false),
                    MapMarkerId = table.Column<int>(type: "int", nullable: false),
                    SpecialInfo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_MapMarkerToMarkerGroupRels_MapMarkers_MapMarkerId",
                        column: x => x.MapMarkerId,
                        principalTable: "MapMarkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapMarkerToMarkerGroupRels_MarkerGroups_MarkerGroupId",
                        column: x => x.MarkerGroupId,
                        principalTable: "MarkerGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MapMarkers_MarkerGroupId",
                table: "MapMarkers",
                column: "MarkerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MapMarkerToMarkerGroupRels_MapMarkerId",
                table: "MapMarkerToMarkerGroupRels",
                column: "MapMarkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MapMarkerToMarkerGroupRels_MarkerGroupId",
                table: "MapMarkerToMarkerGroupRels",
                column: "MarkerGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapMarkers_MarkerGroups_MarkerGroupId",
                table: "MapMarkers",
                column: "MarkerGroupId",
                principalTable: "MarkerGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapMarkers_MarkerGroups_MarkerGroupId",
                table: "MapMarkers");

            migrationBuilder.DropTable(
                name: "MapMarkerToMarkerGroupRels");

            migrationBuilder.DropTable(
                name: "MarkerGroups");

            migrationBuilder.DropIndex(
                name: "IX_MapMarkers_MarkerGroupId",
                table: "MapMarkers");

            migrationBuilder.DropColumn(
                name: "MarkerGroupId",
                table: "MapMarkers");
        }
    }
}
