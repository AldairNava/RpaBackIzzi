using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1260 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "EjecucionExtraccionAutomatizados2Prueba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Procesando",
                table: "EjecucionExtraccionAutomatizados2Prueba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nombreCron",
                table: "EjecucionExtraccionAutomatizados2Prueba",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "scheduleExpression",
                table: "EjecucionExtraccionAutomatizados2Prueba",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IP",
                table: "EjecucionExtraccionAutomatizados2Prueba");

            migrationBuilder.DropColumn(
                name: "Procesando",
                table: "EjecucionExtraccionAutomatizados2Prueba");

            migrationBuilder.DropColumn(
                name: "nombreCron",
                table: "EjecucionExtraccionAutomatizados2Prueba");

            migrationBuilder.DropColumn(
                name: "scheduleExpression",
                table: "EjecucionExtraccionAutomatizados2Prueba");
        }
    }
}
