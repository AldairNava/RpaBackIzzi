using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class tablaserietempora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cve_usuario",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "FechaCaptura",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "FechaCompletado",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "Procesando",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Series");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cve_usuario",
                table: "Series",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCaptura",
                table: "Series",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompletado",
                table: "Series",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Procesando",
                table: "Series",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Series",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
