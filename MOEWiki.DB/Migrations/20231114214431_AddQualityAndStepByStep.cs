using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddQualityAndStepByStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDependsByQuality",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStepByStep",
                table: "ItemRecipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Quality",
                table: "ItemProperties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDependsByQuality",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsStepByStep",
                table: "ItemRecipes");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "ItemProperties");
        }
    }
}
