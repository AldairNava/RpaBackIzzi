using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1275 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "juego");

            migrationBuilder.DropTable(
                name: "practica");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "juego",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    FechaCarga = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaVencimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    casoNegocio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_juego", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "practica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    FechaCarga = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaVencimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    casoNegocio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practica", x => x.Id);
                });
        }
    }
}
