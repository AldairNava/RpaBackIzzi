using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class agentedepuracion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgenteDepuracionCNGenerados");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCaptura",
                table: "AgenteDepuracion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompletado",
                table: "AgenteDepuracion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCN",
                table: "AgenteDepuracion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "AgenteDepuracion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCaptura",
                table: "AgenteDepuracion");

            migrationBuilder.DropColumn(
                name: "FechaCompletado",
                table: "AgenteDepuracion");

            migrationBuilder.DropColumn(
                name: "TipoCN",
                table: "AgenteDepuracion");

            migrationBuilder.DropColumn(
                name: "status",
                table: "AgenteDepuracion");

            migrationBuilder.CreateTable(
                name: "AgenteDepuracionCNGenerados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNGenrado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orden = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgenteDepuracionCNGenerados", x => x.Id);
                });
        }
    }
}
