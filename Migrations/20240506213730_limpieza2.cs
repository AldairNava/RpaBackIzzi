using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class limpieza2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotsProcessLimpieza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    ProcesoBotId = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotsProcessLimpieza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotsProcessLimpieza_cat_procesos_ProcesoBotId",
                        column: x => x.ProcesoBotId,
                        principalTable: "cat_procesos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "cat_procesosLimpieza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nameprocess = table.Column<string>(name: "Name_process", type: "nvarchar(max)", nullable: true),
                    usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateAt = table.Column<DateTime>(name: "update_At", type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ulitmoDiaSend = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_procesosLimpieza", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotsProcessLimpieza_ProcesoBotId",
                table: "BotsProcessLimpieza",
                column: "ProcesoBotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotsProcessLimpieza");

            migrationBuilder.DropTable(
                name: "cat_procesosLimpieza");
        }
    }
}
