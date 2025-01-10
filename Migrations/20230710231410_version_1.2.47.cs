using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1247 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ipEquipo",
                table: "BotsProcess");

            migrationBuilder.DropColumn(
                name: "passwordBot",
                table: "BotsProcess");

            migrationBuilder.DropColumn(
                name: "procesoID",
                table: "BotsProcess");

            migrationBuilder.DropColumn(
                name: "usuarioBot",
                table: "BotsProcess");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "cat_procesos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "update_At",
                table: "cat_procesos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user",
                table: "cat_procesos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcesoBotId",
                table: "BotsProcess",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BotsProcess_ProcesoBotId",
                table: "BotsProcess",
                column: "ProcesoBotId");

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

            migrationBuilder.DropIndex(
                name: "IX_BotsProcess_ProcesoBotId",
                table: "BotsProcess");

            migrationBuilder.DropColumn(
                name: "password",
                table: "cat_procesos");

            migrationBuilder.DropColumn(
                name: "update_At",
                table: "cat_procesos");

            migrationBuilder.DropColumn(
                name: "user",
                table: "cat_procesos");

            migrationBuilder.DropColumn(
                name: "ProcesoBotId",
                table: "BotsProcess");

            migrationBuilder.AddColumn<string>(
                name: "ipEquipo",
                table: "BotsProcess",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "passwordBot",
                table: "BotsProcess",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "procesoID",
                table: "BotsProcess",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "usuarioBot",
                table: "BotsProcess",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
