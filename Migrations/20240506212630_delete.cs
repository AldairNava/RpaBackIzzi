using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotsModel_catalogoProcesosBotsModel_ProcesoBotId",
                table: "BotsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_catalogoProcesosBotsModel",
                table: "catalogoProcesosBotsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotsModel",
                table: "BotsModel");

            migrationBuilder.RenameTable(
                name: "catalogoProcesosBotsModel",
                newName: "cat_procesos");

            migrationBuilder.RenameTable(
                name: "BotsModel",
                newName: "BotsProcess");

            migrationBuilder.RenameIndex(
                name: "IX_BotsModel_ProcesoBotId",
                table: "BotsProcess",
                newName: "IX_BotsProcess_ProcesoBotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cat_procesos",
                table: "cat_procesos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotsProcess",
                table: "BotsProcess",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BotsProcess_cat_procesos_ProcesoBotId",
                table: "BotsProcess",
                column: "ProcesoBotId",
                principalTable: "cat_procesos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotsProcess_cat_procesos_ProcesoBotId",
                table: "BotsProcess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cat_procesos",
                table: "cat_procesos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotsProcess",
                table: "BotsProcess");

            migrationBuilder.RenameTable(
                name: "cat_procesos",
                newName: "catalogoProcesosBotsModel");

            migrationBuilder.RenameTable(
                name: "BotsProcess",
                newName: "BotsModel");

            migrationBuilder.RenameIndex(
                name: "IX_BotsProcess_ProcesoBotId",
                table: "BotsModel",
                newName: "IX_BotsModel_ProcesoBotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_catalogoProcesosBotsModel",
                table: "catalogoProcesosBotsModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotsModel",
                table: "BotsModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BotsModel_catalogoProcesosBotsModel_ProcesoBotId",
                table: "BotsModel",
                column: "ProcesoBotId",
                principalTable: "catalogoProcesosBotsModel",
                principalColumn: "Id");
        }
    }
}
