using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFinal",
                table: "EjecucionExtraccion");

            migrationBuilder.DropColumn(
                name: "FechaInicial",
                table: "EjecucionExtraccion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinal",
                table: "EjecucionExtraccion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicial",
                table: "EjecucionExtraccion",
                type: "datetime2",
                nullable: true);
        }
    }
}
