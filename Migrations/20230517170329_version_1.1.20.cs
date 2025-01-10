using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1120 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FechaCompletado1",
                table: "EjecucionDepuracion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FechaExtraccion1",
                table: "EjecucionDepuracion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fecha_Registro1",
                table: "BasesDepuracion",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCompletado1",
                table: "EjecucionDepuracion");

            migrationBuilder.DropColumn(
                name: "FechaExtraccion1",
                table: "EjecucionDepuracion");

            migrationBuilder.DropColumn(
                name: "Fecha_Registro1",
                table: "BasesDepuracion");
        }
    }
}
