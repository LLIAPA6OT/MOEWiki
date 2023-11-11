using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class AddedTwoFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdateDate",
                table: "Items",
                newName: "RecipesLastUpdateDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "PropertiesLastUpdateDate",
                table: "Items",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "AutoParses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertiesLastUpdateDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "AutoParses");

            migrationBuilder.RenameColumn(
                name: "RecipesLastUpdateDate",
                table: "Items",
                newName: "LastUpdateDate");
        }
    }
}
