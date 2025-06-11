using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class agentedepuracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgenteDepuracion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNGenrado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgenteDepuracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgenteDepuracionCNGenerados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNGenrado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgenteDepuracionCNGenerados", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgenteDepuracion");

            migrationBuilder.DropTable(
                name: "AgenteDepuracionCNGenerados");
        }
    }
}
