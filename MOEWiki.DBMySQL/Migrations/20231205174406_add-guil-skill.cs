using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addguilskill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MapMarkerToMarkerGroupRels",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "MapMarkerToMarkerGroupRels",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "MapMarkerToMarkerGroupRels",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapMarkerToMarkerGroupRels",
                table: "MapMarkerToMarkerGroupRels",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MapMarkerToMarkerGroupRels",
                table: "MapMarkerToMarkerGroupRels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MapMarkerToMarkerGroupRels");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "MapMarkerToMarkerGroupRels");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "MapMarkerToMarkerGroupRels");
        }
    }
}
