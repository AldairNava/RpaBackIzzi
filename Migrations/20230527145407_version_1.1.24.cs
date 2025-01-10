using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AjustesBasesCasosNeogcioCobranza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    casoNegocio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaApertura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    mediosContacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    solucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    submotivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesBasesCasosNeogcioCobranza", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjustesBasesCasosNeogcioCobranza");
        }
    }
}
