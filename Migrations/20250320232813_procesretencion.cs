using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class procesretencion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotsProcessRetencion",
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
                    table.PrimaryKey("PK_BotsProcessRetencion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotsProcessRetencion_cat_procesos_ProcesoBotId",
                        column: x => x.ProcesoBotId,
                        principalTable: "cat_procesos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "cat_procesosRetencion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nameprocess = table.Column<string>(name: "Name_process", type: "nvarchar(max)", nullable: true),
                    Nameusuario = table.Column<string>(name: "Name_usuario", type: "nvarchar(max)", nullable: true),
                    usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateAt = table.Column<DateTime>(name: "update_At", type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ulitmoDiaSend = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_procesosRetencion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotsProcessRetencion_ProcesoBotId",
                table: "BotsProcessRetencion",
                column: "ProcesoBotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotsProcessRetencion");

            migrationBuilder.DropTable(
                name: "cat_procesosRetencion");
        }
    }
}
