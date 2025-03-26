using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class depuracionnotdone1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CIUDAD",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CUENTA",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DIRECCION",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ESTADO_ORDEN",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FECHA_APERTURA",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FECHA_SOLICITADA",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HUB",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MOTIVO_ORDEN",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOMBRE_CLIENTE",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NUMERO_ORDEN",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PAQUETE",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PLAZA",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RPT",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SUBTIPO_CLIENTE",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SUBTIPO_ORDEN",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_CLIENTE",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_ORDEN",
                table: "DepuracionNotdone",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CIUDAD",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "CUENTA",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "DIRECCION",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "ESTADO_ORDEN",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "FECHA_APERTURA",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "FECHA_SOLICITADA",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "HUB",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "MOTIVO_ORDEN",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "NOMBRE_CLIENTE",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "NUMERO_ORDEN",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "PAQUETE",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "PLAZA",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "RPT",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "SUBTIPO_CLIENTE",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "SUBTIPO_ORDEN",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "TIPO_CLIENTE",
                table: "DepuracionNotdone");

            migrationBuilder.DropColumn(
                name: "TIPO_ORDEN",
                table: "DepuracionNotdone");
        }
    }
}
