using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1273 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "juego");

            migrationBuilder.CreateTable(
                name: "practica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    casoNegocio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCarga = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaVencimiento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practica", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "practica");

            migrationBuilder.CreateTable(
                name: "juego",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ejemplo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ejemplo2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_juego", x => x.Id);
                });
        }
    }
}
