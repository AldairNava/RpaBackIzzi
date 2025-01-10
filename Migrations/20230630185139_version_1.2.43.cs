﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1243 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MigracionesLinealeS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    beneficioOtorgado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    canal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaBenOtorgada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEnvioCiber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaRecibidoBO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCarga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    os = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paqueteOrigen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subMotivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCaptura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MigracionesLinealeS", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MigracionesLinealeS");
        }
    }
}
