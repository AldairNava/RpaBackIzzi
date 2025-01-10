using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1261 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cat_extraccionesAutomatizadasModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    submotivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    solucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivosCliente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_extraccionesAutomatizadasModel", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cat_extraccionesAutomatizadasModel");
        }
    }
}
