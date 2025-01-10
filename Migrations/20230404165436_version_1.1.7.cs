using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version117 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "depuracionExportSiebelCanceladas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sucursal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Segmento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaOriginal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InstalacionExpress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubMotivoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaOrden = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaSolicitada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Referido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroVTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveVendedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCNR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubEstado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroCuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaPrecios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdenPedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaPrepago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoCuentaPrepago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VelocidadPrepago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoTipoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AplicaTablet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveTecnicoPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prioridad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoCancelacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferidoLibroTrabajoTransacciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sistema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdenPortabilidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVTipoConexion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAdmision = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoAdmision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FallaGeneralAsociada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RXDS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SNRDS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SNRUP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMDSCER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMUSCER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoEscenario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PuntosTecnicos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TXUP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoEmta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtaEspecial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmacionInstalacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoAsignacionCredito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstatusActividades = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumProgramaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aprobado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AprobadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletadaPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoReprogramacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnviadaPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cerrado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MensalidadTotal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CICPotencial = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depuracionExportSiebelCanceladas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "depuracionExportSiebelCanceladas");
        }
    }
}
