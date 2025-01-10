using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "lead_id",
                table: "DepuracionBasesCanceladasOsExt",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "lead_id",
                table: "DepuracionBasesCanceladasOs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lead_id",
                table: "DepuracionBasesCanceladasOsExt");

            migrationBuilder.DropColumn(
                name: "lead_id",
                table: "DepuracionBasesCanceladasOs");
        }
    }
}
