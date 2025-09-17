using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class flag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cuenta",
                table: "flagConfirmacion",
                newName: "cuenta");

            migrationBuilder.RenameColumn(
                name: "nombreCampaña",
                table: "flagConfirmacion",
                newName: "nombreCampana");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cuenta",
                table: "flagConfirmacion",
                newName: "Cuenta");

            migrationBuilder.RenameColumn(
                name: "nombreCampana",
                table: "flagConfirmacion",
                newName: "nombreCampaña");
        }
    }
}
