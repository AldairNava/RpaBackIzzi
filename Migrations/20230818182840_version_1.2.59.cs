using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1259 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nombreCron",
                table: "EjecucionExtraccionAutomatizadosPrueba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "scheduleExpression",
                table: "EjecucionExtraccionAutomatizadosPrueba",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombreCron",
                table: "EjecucionExtraccionAutomatizadosPrueba");

            migrationBuilder.DropColumn(
                name: "scheduleExpression",
                table: "EjecucionExtraccionAutomatizadosPrueba");
        }
    }
}
