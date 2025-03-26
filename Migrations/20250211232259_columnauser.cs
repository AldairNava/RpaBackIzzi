using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class columnauser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usuario",
                table: "BotsProcessLimpieza");

            migrationBuilder.AddColumn<string>(
                name: "ultimo_usuario",
                table: "cat_procesos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ultimo_usuario",
                table: "cat_procesos");

            migrationBuilder.AddColumn<string>(
                name: "usuario",
                table: "BotsProcessLimpieza",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
