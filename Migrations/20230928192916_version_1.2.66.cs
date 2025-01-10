using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1266 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "correo",
                table: "EjecucionExtraccionAutomatizadosPrueba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "medioExtraccion",
                table: "EjecucionExtraccionAutomatizadosPrueba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "correo",
                table: "EjecucionExtraccionAutomatizados2Prueba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "medioExtraccion",
                table: "EjecucionExtraccionAutomatizados2Prueba",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "correo",
                table: "EjecucionExtraccionAutomatizadosPrueba");

            migrationBuilder.DropColumn(
                name: "medioExtraccion",
                table: "EjecucionExtraccionAutomatizadosPrueba");

            migrationBuilder.DropColumn(
                name: "correo",
                table: "EjecucionExtraccionAutomatizados2Prueba");

            migrationBuilder.DropColumn(
                name: "medioExtraccion",
                table: "EjecucionExtraccionAutomatizados2Prueba");
        }
    }
}
