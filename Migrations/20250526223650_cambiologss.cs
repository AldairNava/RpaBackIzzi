using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class cambiologss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_Reprocesado",
                table: "LogCambioStatus",
                newName: "id_Cambiado");

            migrationBuilder.RenameColumn(
                name: "FechaReproceso",
                table: "LogCambioStatus",
                newName: "FechaCambio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_Cambiado",
                table: "LogCambioStatus",
                newName: "id_Reprocesado");

            migrationBuilder.RenameColumn(
                name: "FechaCambio",
                table: "LogCambioStatus",
                newName: "FechaReproceso");
        }
    }
}
