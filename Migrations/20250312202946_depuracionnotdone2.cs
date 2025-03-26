using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class depuracionnotdone2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CnGenerado",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "Cve_usuario",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "FechaCaptura",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "FechaCompletado",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DepuracionNotdone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CnGenerado",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cve_usuario",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCaptura",
                table: "DepuracionNotdone",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompletado",
                table: "DepuracionNotdone",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
