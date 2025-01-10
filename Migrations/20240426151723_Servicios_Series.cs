using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ServiciosSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fechaExpiracionPws");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompletado",
                table: "Series",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCompletado",
                table: "Series");

            migrationBuilder.CreateTable(
                name: "fechaExpiracionPws",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiasRestantes = table.Column<int>(type: "int", nullable: true),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    pws = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateAt = table.Column<DateTime>(name: "update_At", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fechaExpiracionPws", x => x.Id);
                });
        }
    }
}
