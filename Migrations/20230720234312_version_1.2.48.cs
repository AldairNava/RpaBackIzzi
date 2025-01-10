using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1248 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "estatusAjuste",
                table: "AjustesSinValidacion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "estatusAjuste",
                table: "AjustesBasesCasosNeogcioCobranza",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estatusAjuste",
                table: "AjustesSinValidacion");

            migrationBuilder.DropColumn(
                name: "estatusAjuste",
                table: "AjustesBasesCasosNeogcioCobranza");
        }
    }
}
