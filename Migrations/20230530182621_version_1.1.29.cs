using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1129 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cnGenerado",
                table: "AjustesBasesCasosNeogcioCobranza",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "statusNegocioGenerado",
                table: "AjustesBasesCasosNeogcioCobranza",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cnGenerado",
                table: "AjustesBasesCasosNeogcioCobranza");

            migrationBuilder.DropColumn(
                name: "statusNegocioGenerado",
                table: "AjustesBasesCasosNeogcioCobranza");
        }
    }
}
