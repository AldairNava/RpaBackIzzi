using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version114 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EjecucionDepuracion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoExtraccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaExtraccion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Archivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    HorarioInicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EjecucionDepuracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HorariosReporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosReporte", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EjecucionDepuracion");

            migrationBuilder.DropTable(
                name: "HorariosReporte");
        }
    }
}
