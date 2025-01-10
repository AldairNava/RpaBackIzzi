using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1250 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cat_procesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nameprocess = table.Column<string>(name: "Name_process", type: "nvarchar(max)", nullable: true),
                    user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateAt = table.Column<DateTime>(name: "update_At", type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_procesos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BotsProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcesoBotId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotsProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotsProcess_cat_procesos_ProcesoBotId",
                        column: x => x.ProcesoBotId,
                        principalTable: "cat_procesos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotsProcess_ProcesoBotId",
                table: "BotsProcess",
                column: "ProcesoBotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotsProcess");

            migrationBuilder.DropTable(
                name: "cat_procesos");
        }
    }
}
