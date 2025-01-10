using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepuracionBasesCanceladasOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compania = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cngenerado = table.Column<string>(name: "Cn_generado", type: "nvarchar(max)", nullable: true),
                    FechaHoraCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCarga = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepuracionBasesCanceladasOs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepuracionBasesCanceladasOs");
        }
    }
}
