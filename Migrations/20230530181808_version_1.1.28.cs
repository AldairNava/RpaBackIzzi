using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1128 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "casoNegocio",
                table: "EjecucionNotDone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "statusCasoNegocio",
                table: "EjecucionNotDone",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "casoNegocio",
                table: "EjecucionNotDone");

            migrationBuilder.DropColumn(
                name: "statusCasoNegocio",
                table: "EjecucionNotDone");
        }
    }
}
