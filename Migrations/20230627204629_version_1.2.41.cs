using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancelacionSinValidacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    comentariosOs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCaptura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelacionSinValidacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CasosNegocioSinValidacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    submotivos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    solucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCaptura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasosNegocioSinValidacion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelacionSinValidacion");

            migrationBuilder.DropTable(
                name: "CasosNegocioSinValidacion");
        }
    }
}
