using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class basesdel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCompletado",
                table: "BasesCC");

            migrationBuilder.DropColumn(
                name: "IP",
                table: "BasesCC");

            migrationBuilder.DropColumn(
                name: "Procesando",
                table: "BasesCC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompletado",
                table: "BasesCC",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "BasesCC",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Procesando",
                table: "BasesCC",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
