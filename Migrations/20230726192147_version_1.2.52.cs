using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tipoOrden",
                table: "EjecucionNotDone",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "clasificacionOrden",
                table: "EjecucionNotDone",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "EjecucionNotDone",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EjecucionNotDone_clasificacionOrden",
                table: "EjecucionNotDone",
                column: "clasificacionOrden");

            migrationBuilder.CreateIndex(
                name: "IX_EjecucionNotDone_Status",
                table: "EjecucionNotDone",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_EjecucionNotDone_tipoOrden",
                table: "EjecucionNotDone",
                column: "tipoOrden");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EjecucionNotDone_clasificacionOrden",
                table: "EjecucionNotDone");

            migrationBuilder.DropIndex(
                name: "IX_EjecucionNotDone_Status",
                table: "EjecucionNotDone");

            migrationBuilder.DropIndex(
                name: "IX_EjecucionNotDone_tipoOrden",
                table: "EjecucionNotDone");

            migrationBuilder.AlterColumn<string>(
                name: "tipoOrden",
                table: "EjecucionNotDone",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "clasificacionOrden",
                table: "EjecucionNotDone",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "EjecucionNotDone",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
