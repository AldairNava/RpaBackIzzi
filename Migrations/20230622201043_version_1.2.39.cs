using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1239 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrefijosRegionDepuracion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subregion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plaza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hubOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prefijo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cveusuario = table.Column<string>(name: "Cve_usuario", type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrefijosRegionDepuracion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrefijosRegionDepuracion");
        }
    }
}
