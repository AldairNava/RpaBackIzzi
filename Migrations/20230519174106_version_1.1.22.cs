using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1122 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepuracionBasesCanceladasOsExt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compania = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaTecnico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HUB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuienResponde = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transferir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreContacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComentariosCyber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timecarga = table.Column<string>(name: "time_carga", type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaHoraCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cngenerado = table.Column<string>(name: "cn_generado", type: "nvarchar(max)", nullable: true),
                    usuariocreo = table.Column<string>(name: "usuario_creo", type: "nvarchar(max)", nullable: true),
                    Userregistro = table.Column<string>(name: "User_registro", type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepuracionBasesCanceladasOsExt", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepuracionBasesCanceladasOsExt");
        }
    }
}
