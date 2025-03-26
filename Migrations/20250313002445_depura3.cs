using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class depura3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepuracionNotDoneOriginal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CanalDeIngreso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoAdmision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAdmision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDeCuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoTelefonoPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefonos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoEMTA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtaEspecial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoDeLaOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdenDePortabilidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoDeLaCancelacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sistema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubEstado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoVTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveVendedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MensualidadTotal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDeCNR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentoDePrueba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoEnFecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoDeTipoDeOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaDeFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferidoAlLibroDeTrabajoDeTransacciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaDeLaOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AplicaTablet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmacionDeInstalacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroDeOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveDelTecnicoPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoDeAsignacionDeCredito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoProgramaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compania = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Centro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaDeImpuestos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prioridad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroDeCuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aprobado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AprobadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaDePrecios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PorcentajeDeDescuento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletadaPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoDeReprogramacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnviadaPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaSolicitada = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepuracionNotDoneOriginal", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepuracionNotDoneOriginal");
        }
    }
}
