using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class depuracionnotdone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepuracionNotdone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CnGenerado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCaptura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_DepuracionNotdone", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepuracionNotdone");
        }
    }
}
