using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class estatsnotdone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadisticasNotDonestats",
                columns: table => new
                {
                    Completado = table.Column<int>(type: "int", nullable: true),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadisticasNotDonestats");
        }
    }
}
