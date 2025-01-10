using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1127 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotsProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ipEquipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    procesoID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usuarioBot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordBot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotsProcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cat_procesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nameprocess = table.Column<string>(name: "Name_process", type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_procesos", x => x.Id);
                });
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
