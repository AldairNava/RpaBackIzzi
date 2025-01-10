using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class tablapws : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fechaExpiracionPws_cat_procesos_ProcesoBotActualizadoId",
                table: "fechaExpiracionPws");

            migrationBuilder.DropIndex(
                name: "IX_fechaExpiracionPws_ProcesoBotActualizadoId",
                table: "fechaExpiracionPws");

            migrationBuilder.DropColumn(
                name: "ProcesoBotActualizadoId",
                table: "fechaExpiracionPws");

            migrationBuilder.DropColumn(
                name: "cat_procesosId",
                table: "fechaExpiracionPws");

            migrationBuilder.AddColumn<int>(
                name: "pws",
                table: "fechaExpiracionPws",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pws",
                table: "fechaExpiracionPws");

            migrationBuilder.AddColumn<int>(
                name: "ProcesoBotActualizadoId",
                table: "fechaExpiracionPws",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cat_procesosId",
                table: "fechaExpiracionPws",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_fechaExpiracionPws_ProcesoBotActualizadoId",
                table: "fechaExpiracionPws",
                column: "ProcesoBotActualizadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_fechaExpiracionPws_cat_procesos_ProcesoBotActualizadoId",
                table: "fechaExpiracionPws",
                column: "ProcesoBotActualizadoId",
                principalTable: "cat_procesos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
