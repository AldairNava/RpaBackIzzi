using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1133 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AjustesNotDone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoCasoNegocio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesNotDone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AjustesTiempoAjuste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tiempoAjuste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesTiempoAjuste", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjustesNotDone");

            migrationBuilder.DropTable(
                name: "AjustesTiempoAjuste");
        }
    }
}
