using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1126 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EjecucionNotDone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estadoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaApertura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaSolicitada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    hub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoCancelacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoReprogramacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numRepro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paquete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    perfilPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plaza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    referido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    situacionAnticipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subtipoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subtipoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tecnico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCaptura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EjecucionNotDone", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EjecucionNotDone");
        }
    }
}
