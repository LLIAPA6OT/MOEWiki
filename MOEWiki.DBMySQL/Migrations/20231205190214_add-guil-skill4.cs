using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addguilskill4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopperCoins",
                table: "GuildSkills");

            migrationBuilder.DropColumn(
                name: "GuildActivity",
                table: "GuildSkills");

            migrationBuilder.DropColumn(
                name: "GuildLevel",
                table: "GuildSkills");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "GuildSkills");

            migrationBuilder.DropColumn(
                name: "PrevLevel",
                table: "GuildSkills");

            migrationBuilder.CreateTable(
                name: "GuildSkillRequires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GuildSkillId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    GuildLevel = table.Column<int>(type: "int", nullable: false),
                    GuildActivity = table.Column<int>(type: "int", nullable: false),
                    CopperCoins = table.Column<int>(type: "int", nullable: false),
                    PrevLevel = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildSkillRequires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildSkillRequires_GuildSkills_GuildSkillId",
                        column: x => x.GuildSkillId,
                        principalTable: "GuildSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GuildSkillRequires_GuildSkillId",
                table: "GuildSkillRequires",
                column: "GuildSkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildSkillRequires");

            migrationBuilder.AddColumn<int>(
                name: "CopperCoins",
                table: "GuildSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuildActivity",
                table: "GuildSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuildLevel",
                table: "GuildSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "GuildSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrevLevel",
                table: "GuildSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
