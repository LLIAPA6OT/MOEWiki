using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addguilskill3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_GuildSkillRelations_GuildSkillId",
                table: "GuildSkillRelations",
                column: "GuildSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildSkillEffects_GuildSkillId",
                table: "GuildSkillEffects",
                column: "GuildSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuildSkillEffects_GuildSkills_GuildSkillId",
                table: "GuildSkillEffects",
                column: "GuildSkillId",
                principalTable: "GuildSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuildSkillRelations_GuildSkills_GuildSkillId",
                table: "GuildSkillRelations",
                column: "GuildSkillId",
                principalTable: "GuildSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuildSkillEffects_GuildSkills_GuildSkillId",
                table: "GuildSkillEffects");

            migrationBuilder.DropForeignKey(
                name: "FK_GuildSkillRelations_GuildSkills_GuildSkillId",
                table: "GuildSkillRelations");

            migrationBuilder.DropIndex(
                name: "IX_GuildSkillRelations_GuildSkillId",
                table: "GuildSkillRelations");

            migrationBuilder.DropIndex(
                name: "IX_GuildSkillEffects_GuildSkillId",
                table: "GuildSkillEffects");

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
                    CopperCoins = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GuildActivity = table.Column<int>(type: "int", nullable: false),
                    GuildLevel = table.Column<int>(type: "int", nullable: false),
                    GuildSkillId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    PrevLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildSkillRequires", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
