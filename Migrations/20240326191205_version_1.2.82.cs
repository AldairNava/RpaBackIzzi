using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1282 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCompletado",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Series");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompletado",
                table: "Series",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ip",
                table: "Series",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
