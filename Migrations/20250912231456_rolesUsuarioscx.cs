using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class rolesUsuarioscx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flagConfirmacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ordenGeneada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCaptura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    Cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    servicioContratado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaInstalacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombreCampaña = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opcionDigitada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dnis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flagConfirmacion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flagConfirmacion");
        }
    }
}
