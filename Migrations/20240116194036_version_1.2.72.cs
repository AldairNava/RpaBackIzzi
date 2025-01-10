using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1272 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ulitmoDiaSend",
                table: "cat_procesos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstadisticasAjustesConValidacion",
                columns: table => new
                {
                    Ajusterealizado = table.Column<int>(name: "Ajuste realizado", type: "int", nullable: true),
                    ErrorOperativo = table.Column<int>(name: "Error Operativo", type: "int", nullable: true),
                    Registropendiente = table.Column<int>(name: "Registro pendiente", type: "int", nullable: true),
                    InconsistenciaenSiebel = table.Column<int>(name: "Inconsistencia en Siebel", type: "int", nullable: true),
                    FallaRPA = table.Column<int>(name: "Falla RPA", type: "int", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: true),
                    @base = table.Column<string>(name: "base", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "EstadisticasModel",
                columns: table => new
                {
                    FallaRPA = table.Column<int>(name: "Falla RPA", type: "int", nullable: true),
                    ErrorOperativo = table.Column<int>(name: "Error Operativo", type: "int", nullable: true),
                    Registropendiente = table.Column<int>(name: "Registro pendiente", type: "int", nullable: true),
                    Registroexitoso = table.Column<int>(name: "Registro exitoso", type: "int", nullable: true),
                    Ordencancelada = table.Column<int>(name: "Orden cancelada", type: "int", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: true),
                    @base = table.Column<string>(name: "base", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadisticasAjustesConValidacion");

            migrationBuilder.DropTable(
                name: "EstadisticasModel");

            migrationBuilder.DropColumn(
                name: "ulitmoDiaSend",
                table: "cat_procesos");
        }
    }
}
