using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1263 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tipoProgramacion",
                table: "EjecucionExtraccionAutomatizadosPrueba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tipoProgramacion",
                table: "EjecucionExtraccionAutomatizados2Prueba",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipoProgramacion",
                table: "EjecucionExtraccionAutomatizadosPrueba");

            migrationBuilder.DropColumn(
                name: "tipoProgramacion",
                table: "EjecucionExtraccionAutomatizados2Prueba");
        }
    }
}
