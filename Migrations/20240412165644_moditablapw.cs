using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class moditablapw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fechaExpiracionPws_cat_procesos_catalogoProcesosBotsModelId",
                table: "fechaExpiracionPws");

            migrationBuilder.DropIndex(
                name: "IX_fechaExpiracionPws_catalogoProcesosBotsModelId",
                table: "fechaExpiracionPws");

            migrationBuilder.RenameColumn(
                name: "catalogoProcesosBotsModelId",
                table: "fechaExpiracionPws",
                newName: "cat_procesosId");

            migrationBuilder.AddColumn<int>(
                name: "ProcesoBotActualizadoId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "cat_procesosId",
                table: "fechaExpiracionPws",
                newName: "catalogoProcesosBotsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_fechaExpiracionPws_catalogoProcesosBotsModelId",
                table: "fechaExpiracionPws",
                column: "catalogoProcesosBotsModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_fechaExpiracionPws_cat_procesos_catalogoProcesosBotsModelId",
                table: "fechaExpiracionPws",
                column: "catalogoProcesosBotsModelId",
                principalTable: "cat_procesos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
