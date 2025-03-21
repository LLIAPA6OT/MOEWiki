﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    /// <inheritdoc />
    public partial class addcolumninguildskill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "GuildSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Column",
                table: "GuildSkills");
        }
    }
}
