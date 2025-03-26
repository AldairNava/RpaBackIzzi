using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class DepuracionNotdoneFinal2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepuracionNotdoneFinal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUENTA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOMBRECLIENTE = table.Column<string>(name: "NOMBRE_CLIENTE", type: "nvarchar(max)", nullable: true),
                    TIPOCLIENTE = table.Column<string>(name: "TIPO_CLIENTE", type: "nvarchar(max)", nullable: true),
                    SUBTIPOCLIENTE = table.Column<string>(name: "SUBTIPO_CLIENTE", type: "nvarchar(max)", nullable: true),
                    DIRECCION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TIPOORDEN = table.Column<string>(name: "TIPO_ORDEN", type: "nvarchar(max)", nullable: true),
                    SUBTIPOORDEN = table.Column<string>(name: "SUBTIPO_ORDEN", type: "nvarchar(max)", nullable: true),
                    PAQUETE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUMEROORDEN = table.Column<string>(name: "NUMERO_ORDEN", type: "nvarchar(max)", nullable: true),
                    ESTADOORDEN = table.Column<string>(name: "ESTADO_ORDEN", type: "nvarchar(max)", nullable: true),
                    FECHAAPERTURA = table.Column<DateTime>(name: "FECHA_APERTURA", type: "datetime2", nullable: true),
                    FECHASOLICITADA = table.Column<DateTime>(name: "FECHA_SOLICITADA", type: "datetime2", nullable: true),
                    MOTIVOORDEN = table.Column<string>(name: "MOTIVO_ORDEN", type: "nvarchar(max)", nullable: true),
                    HUB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIUDAD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLAZA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VENDEDOR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TECNICO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREADOPOR = table.Column<string>(name: "CREADO_POR", type: "nvarchar(max)", nullable: true),
                    ULTIMAMODPOR = table.Column<string>(name: "ULTIMA_MOD_POR", type: "nvarchar(max)", nullable: true),
                    REFERIDO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUMREPRO = table.Column<string>(name: "NUM_REPRO", type: "nvarchar(max)", nullable: true),
                    MOTIVOREPROGRAMACION = table.Column<string>(name: "MOTIVO_REPROGRAMACION", type: "nvarchar(max)", nullable: true),
                    MOTIVOCANCELACION = table.Column<string>(name: "MOTIVO_CANCELACION", type: "nvarchar(max)", nullable: true),
                    SITUACIONANTICIPO = table.Column<string>(name: "SITUACION_ANTICIPO", type: "nvarchar(max)", nullable: true),
                    PERFILPAGO = table.Column<string>(name: "PERFIL_PAGO", type: "nvarchar(max)", nullable: true),
                    COMENTARIOS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEL1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEL2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEL3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEL4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepuracionNotdoneFinal", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepuracionNotdoneFinal");
        }
    }
}
