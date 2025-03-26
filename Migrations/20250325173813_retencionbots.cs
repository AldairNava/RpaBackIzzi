using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class retencionbots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotsProcessLimpieza_cat_procesos_ProcesoBotId",
                table: "BotsProcessLimpieza");

            migrationBuilder.DropForeignKey(
                name: "FK_BotsProcessRetencion_cat_procesos_ProcesoBotId",
                table: "BotsProcessRetencion");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
    WHERE name = 'FK_BotsProcessLimpieza_cat_procesosLimpieza_ProcesoBotId'
      AND parent_object_id = OBJECT_ID('BotsProcessLimpieza'))
BEGIN
    ALTER TABLE BotsProcessLimpieza 
    ADD CONSTRAINT FK_BotsProcessLimpieza_cat_procesosLimpieza_ProcesoBotId
    FOREIGN KEY (ProcesoBotId) REFERENCES cat_procesosLimpieza(Id)
END");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
    WHERE name = 'FK_BotsProcessRetencion_cat_procesosRetencion_ProcesoBotId'
      AND parent_object_id = OBJECT_ID('BotsProcessRetencion'))
BEGIN
    ALTER TABLE BotsProcessRetencion 
    ADD CONSTRAINT FK_BotsProcessRetencion_cat_procesosRetencion_ProcesoBotId
    FOREIGN KEY (ProcesoBotId) REFERENCES cat_procesosRetencion(Id)
END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS (SELECT * FROM sys.foreign_keys 
    WHERE name = 'FK_BotsProcessLimpieza_cat_procesosLimpieza_ProcesoBotId'
      AND parent_object_id = OBJECT_ID('BotsProcessLimpieza'))
BEGIN
    ALTER TABLE BotsProcessLimpieza DROP CONSTRAINT FK_BotsProcessLimpieza_cat_procesosLimpieza_ProcesoBotId
END");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT * FROM sys.foreign_keys 
    WHERE name = 'FK_BotsProcessRetencion_cat_procesosRetencion_ProcesoBotId'
      AND parent_object_id = OBJECT_ID('BotsProcessRetencion'))
BEGIN
    ALTER TABLE BotsProcessRetencion DROP CONSTRAINT FK_BotsProcessRetencion_cat_procesosRetencion_ProcesoBotId
END");

            migrationBuilder.AddForeignKey(
                name: "FK_BotsProcessLimpieza_cat_procesos_ProcesoBotId",
                table: "BotsProcessLimpieza",
                column: "ProcesoBotId",
                principalTable: "cat_procesos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BotsProcessRetencion_cat_procesos_ProcesoBotId",
                table: "BotsProcessRetencion",
                column: "ProcesoBotId",
                principalTable: "cat_procesos",
                principalColumn: "Id");
        }
    }
}
