using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1264 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cat_extraccionesAutomatizadasOSActModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoOrdenOS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estadoOS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoAct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    areaConocimientoAct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estadoAct = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_extraccionesAutomatizadasOSActModel", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cat_extraccionesAutomatizadasOSActModel");
        }
    }
}
