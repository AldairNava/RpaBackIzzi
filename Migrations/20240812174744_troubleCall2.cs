using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class troubleCall2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroSolicitud",
                table: "OrdenTroubleCall",
                newName: "NumeroOrden");

            migrationBuilder.AddColumn<string>(
                name: "NumeroCN",
                table: "OrdenTroubleCall",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroCN",
                table: "OrdenTroubleCall");

            migrationBuilder.RenameColumn(
                name: "NumeroOrden",
                table: "OrdenTroubleCall",
                newName: "NumeroSolicitud");
        }
    }
}
