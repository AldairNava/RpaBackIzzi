using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1116 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "comentariosCN",
                table: "AjustesCambioServicios",
                newName: "motivoCliente");

            migrationBuilder.RenameColumn(
                name: "comentarios",
                table: "AjustesCambioServicios",
                newName: "motivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "motivoCliente",
                table: "AjustesCambioServicios",
                newName: "comentariosCN");

            migrationBuilder.RenameColumn(
                name: "motivo",
                table: "AjustesCambioServicios",
                newName: "comentarios");
        }
    }
}
