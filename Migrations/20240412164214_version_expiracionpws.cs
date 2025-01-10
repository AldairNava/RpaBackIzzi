using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class versionexpiracionpws : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fechaExpiracionPws",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    catalogoProcesosBotsModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fechaExpiracionPws", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fechaExpiracionPws_cat_procesos_catalogoProcesosBotsModelId",
                        column: x => x.catalogoProcesosBotsModelId,
                        principalTable: "cat_procesos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fechaExpiracionPws_catalogoProcesosBotsModelId",
                table: "fechaExpiracionPws",
                column: "catalogoProcesosBotsModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fechaExpiracionPws");
        }
    }
}
