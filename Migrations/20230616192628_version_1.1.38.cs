using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1138 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "casoNegocio",
                table: "AjustesSinValidacion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "numeroAjuste",
                table: "AjustesSinValidacion",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "casoNegocio",
                table: "AjustesSinValidacion");

            migrationBuilder.DropColumn(
                name: "numeroAjuste",
                table: "AjustesSinValidacion");
        }
    }
}
