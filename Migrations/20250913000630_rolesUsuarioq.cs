using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class rolesUsuarioq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "servicioContratado",
                table: "flagConfirmacion",
                newName: "ServicioContratado");

            migrationBuilder.RenameColumn(
                name: "ordenGeneada",
                table: "flagConfirmacion",
                newName: "OrdenGeneada");

            migrationBuilder.RenameColumn(
                name: "opcionDigitada",
                table: "flagConfirmacion",
                newName: "OpcionDigitada");

            migrationBuilder.RenameColumn(
                name: "fechaInstalacion",
                table: "flagConfirmacion",
                newName: "FechaInstalacion");

            migrationBuilder.RenameColumn(
                name: "dnis",
                table: "flagConfirmacion",
                newName: "DNIS");

            migrationBuilder.RenameColumn(
                name: "cuenta",
                table: "flagConfirmacion",
                newName: "Cuenta");

            migrationBuilder.RenameColumn(
                name: "numeroOrden",
                table: "flagConfirmacion",
                newName: "NumOrden");

            migrationBuilder.RenameColumn(
                name: "nombreCliente",
                table: "flagConfirmacion",
                newName: "NomCliente");

            migrationBuilder.RenameColumn(
                name: "nombreCampaña",
                table: "flagConfirmacion",
                newName: "NomCampaña");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServicioContratado",
                table: "flagConfirmacion",
                newName: "servicioContratado");

            migrationBuilder.RenameColumn(
                name: "OrdenGeneada",
                table: "flagConfirmacion",
                newName: "ordenGeneada");

            migrationBuilder.RenameColumn(
                name: "OpcionDigitada",
                table: "flagConfirmacion",
                newName: "opcionDigitada");

            migrationBuilder.RenameColumn(
                name: "FechaInstalacion",
                table: "flagConfirmacion",
                newName: "fechaInstalacion");

            migrationBuilder.RenameColumn(
                name: "DNIS",
                table: "flagConfirmacion",
                newName: "dnis");

            migrationBuilder.RenameColumn(
                name: "Cuenta",
                table: "flagConfirmacion",
                newName: "cuenta");

            migrationBuilder.RenameColumn(
                name: "NumOrden",
                table: "flagConfirmacion",
                newName: "numeroOrden");

            migrationBuilder.RenameColumn(
                name: "NomCliente",
                table: "flagConfirmacion",
                newName: "nombreCliente");

            migrationBuilder.RenameColumn(
                name: "NomCampaña",
                table: "flagConfirmacion",
                newName: "nombreCampaña");
        }
    }
}
