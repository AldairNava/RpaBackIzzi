using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1269 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numeroAjuste",
                table: "NotDoneCreacionOrdenModel",
                newName: "numeroOrden");

            migrationBuilder.RenameColumn(
                name: "estatusAjuste",
                table: "NotDoneCreacionOrdenModel",
                newName: "estatusOrden");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numeroOrden",
                table: "NotDoneCreacionOrdenModel",
                newName: "numeroAjuste");

            migrationBuilder.RenameColumn(
                name: "estatusOrden",
                table: "NotDoneCreacionOrdenModel",
                newName: "estatusAjuste");
        }
    }
}
