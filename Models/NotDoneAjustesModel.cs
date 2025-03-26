namespace WebApplication1.Models
{
    public class NotDoneAjustesModel
        {
            public int Id { get; set; }
            public string? casoNegocio { get; set; }
            public string? categoria { get; set; }
            public string? cuenta { get; set; }
            public string? estado { get; set; }
            public DateTime? fechaApertura { get; set; }
            public string? mediosContacto { get; set; }
            public string? motivoCliente { get; set; }
            public string? motivos { get; set; }
            public string? solucion { get; set; }
            public string? submotivo { get; set; }
            public string? cnGenerado { get; set; }
            public string? statusNegocioGenerado { get; set; }
            public string? numeroAjuste { get; set; }
            public string? Cve_usuario { get; set; }
            public DateTime? FechaCompletado { get; set; }
            public DateTime? FechaCaptura { get; set; }
            public string? Status { get; set; }
            public string? Procesando { get; set; }
            public string? IP { get; set; }
            public string? estatusAjuste { get; set; }
            public string? prioridad { get; set; }
        }

    public class NotDoneModel
        {
            public int Id { get; set; }
            public string? ciudad { get; set; }
            public string? comentarios { get; set; }
            public string? creadoPor { get; set; }
            public string? cuenta { get; set; }
            public string? direccion { get; set; }
            public string? estadoOrden { get; set; }
            public DateTime? fechaApertura { get; set; }
            public DateTime? fechaSolicitada { get; set; }
            public string? hub { get; set; }
            public string? motivoCancelacion { get; set; }
            public string? motivoOrden { get; set; }
            public string? motivoReprogramacion { get; set; }
            public string? nombreCliente { get; set; }
            public string? numeroOrden { get; set; }
            public string? numRepro { get; set; }
            public string? paquete { get; set; }
            public string? perfilPago { get; set; }
            public string? plaza { get; set; }
            public string? referido { get; set; }
            public string? rpt { get; set; }
            public string? situacionAnticipo { get; set; }
            public string? subtipoCliente { get; set; }
            public string? subtipoOrden { get; set; }
            public string? tecnico { get; set; }
            public string? telefono { get; set; }
            public string? tipCliente { get; set; }
            public string? tipoOrden { get; set; }
            public string? UltimaModificacionPor { get; set; }
            public string? Vendedor { get; set; }
            public string? Cve_usuario { get; set; }
            public DateTime? FechaCompletado { get; set; }
            public DateTime? FechaCaptura { get; set; }
            public string? Status { get; set; }
            public string? Procesando { get; set; }
            public string? IP { get; set; }
            public string? casoNegocio { get; set; }
            public string? statusCasoNegocio { get; set; }
            public string? resultadoLlamada { get; set; }
            public string? clasificacionOrden { get; set; }

        }


    public class AjustesTiempoAjusteModel
        {
            public int Id { get; set; }
            public string? tiempoAjuste { get; set; }
            public string? valor { get; set; }
            public DateTime? updatedAt { get; set; }
        }

    public class AjustesNotDoneModel
        {
            public int Id { get; set; }
            public string? numero { get; set; }
            public string? tipoCasoNegocio { get; set; }
            public DateTime? updatedAt { get; set; }
        }

    public class AjustesSinValidacionModel
        {
            public int Id { get; set; }
            public string? cuenta { get; set; }
            public string? motivoAjuste { get; set; }
            public string? comentarioAjuste { get; set; }
            public string? cantidadAjustar { get; set; }
            public string? tipoAplicacion { get; set; }
            public string? numeroAjuste { get; set; }
            public string? casoNegocio { get; set; }

            public string? Cve_usuario { get; set; }
            public DateTime? FechaCompletado { get; set; }
            public DateTime? FechaCaptura { get; set; }
            public string? Status { get; set; }
            public string? Procesando { get; set; }
            public string? IP { get; set; }
            public string? estatusAjuste { get; set; }
        }

    public class CancelacionSinValidacionModel
        {
            public int Id { get; set; }
            public string? cuenta { get; set; }
            public string? numeroOrden { get; set; }
            public string? estado { get; set; }
            public string? comentariosOs { get; set; }
            public string? Cve_usuario { get; set; }
            public DateTime? FechaCompletado { get; set; }
            public DateTime? FechaCaptura { get; set; }
            public string? Status { get; set; }
            public string? Procesando { get; set; }
            public string? IP { get; set; }
        }

    public class CasosNegocioSinValidacionModel
        {
            public int Id { get; set; }
            public string? cuenta { get; set; }
            public string? categoria { get; set; }
            public string? motivos { get; set; }
            public string? submotivos { get; set; }
            public string? solucion { get; set; }
            public string? estado { get; set; }
            public string? motivoCliente { get; set; }
            public string? Cve_usuario { get; set; }
            public string? casoNegocio { get; set; }
            public DateTime? FechaCompletado { get; set; }
            public DateTime? FechaCaptura { get; set; }
            public string? Status { get; set; }
            public string? Procesando { get; set; }
            public string? IP { get; set; }
            public string? comentarios { get; set; }
        }

    public class NotDoneCreacionOrdenModel
        {
            public int Id { get; set; }
            public string? casoNegocio { get; set; }
            public string? categoria { get; set; }
            public string? cuenta { get; set; }
            public string? estado { get; set; }
            public DateTime? fechaApertura { get; set; }
            public string? mediosContacto { get; set; }
            public string? motivoCliente { get; set; }
            public string? motivos { get; set; }
            public string? solucion { get; set; }
            public string? submotivo { get; set; }
            public string? cnGenerado { get; set; }
            public string? statusNegocioGenerado { get; set; }
            public string? numeroOrden { get; set; }
            public string? estatusOrden { get; set; }
            public string? Cve_usuario { get; set; }
            public DateTime? FechaCompletado { get; set; }
            public DateTime? FechaCaptura { get; set; }
            public string? Status { get; set; }
            public string? Procesando { get; set; }
            public string? IP { get; set; }
        }

    public class DepuracionNotdone
        {
            public int Id { get; set; }
            public string? CUENTA { get; set; }
            public string? NOMBRE_CLIENTE { get; set; }
            public string? TIPO_CLIENTE { get; set; }
            public string? SUBTIPO_CLIENTE { get; set; }
            public string? DIRECCION { get; set; }
            public string? TIPO_ORDEN { get; set; }
            public string? SUBTIPO_ORDEN { get; set; }
            public string? PAQUETE { get; set; }
            public string? NUMERO_ORDEN { get; set; }
            public string? ESTADO_ORDEN { get; set; }
            public DateTime? FECHA_APERTURA { get; set; }
            public DateTime? FECHA_SOLICITADA { get; set; }
            public string? MOTIVO_ORDEN { get; set; }
            public string? HUB { get; set; }
            public string? RPT { get; set; }
            public string? CIUDAD { get; set; }
            public string? PLAZA { get; set; }
            public string? VENDEDOR { get; set; }
            public string? TECNICO { get; set; }
            public string? CREADO_POR { get; set; }
            public string? ULTIMA_MOD_POR { get; set; }
            public string? REFERIDO { get; set; }
            public string? NUM_REPRO { get; set; }
            public string? MOTIVO_REPROGRAMACION { get; set; }
            public string? MOTIVO_CANCELACION { get; set; }
            public string? SITUACION_ANTICIPO { get; set; }
            public string? PERFIL_PAGO { get; set; }
            public string? COMENTARIOS { get; set; }
            public string? TEL1 { get; set; }
            public string? TEL2 { get; set; }
            public string? TEL3 { get; set; }
            public string? TEL4 { get; set; }
        }

    public class DepuracionNotdonefinal
        {
            public int Id { get; set; }
            public string? CUENTA { get; set; }
            public string? NOMBRE_CLIENTE { get; set; }
            public string? TIPO_CLIENTE { get; set; }
            public string? SUBTIPO_CLIENTE { get; set; }
            public string? DIRECCION { get; set; }
            public string? TIPO_ORDEN { get; set; }
            public string? SUBTIPO_ORDEN { get; set; }
            public string? PAQUETE { get; set; }
            public string? NUMERO_ORDEN { get; set; }
            public string? ESTADO_ORDEN { get; set; }
            public DateTime? FECHA_APERTURA { get; set; }
            public DateTime? FECHA_SOLICITADA { get; set; }
            public string? MOTIVO_ORDEN { get; set; }
            public string? HUB { get; set; }
            public string? RPT { get; set; }
            public string? CIUDAD { get; set; }
            public string? PLAZA { get; set; }
            public string? VENDEDOR { get; set; }
            public string? TECNICO { get; set; }
            public string? CREADO_POR { get; set; }
            public string? ULTIMA_MOD_POR { get; set; }
            public string? REFERIDO { get; set; }
            public string? NUM_REPRO { get; set; }
            public string? MOTIVO_REPROGRAMACION { get; set; }
            public string? MOTIVO_CANCELACION { get; set; }
            public string? SITUACION_ANTICIPO { get; set; }
            public string? PERFIL_PAGO { get; set; }
            public string? COMENTARIOS { get; set; }
            public string? TEL1 { get; set; }
            public string? TEL2 { get; set; }
            public string? TEL3 { get; set; }
            public string? TEL4 { get; set; }
        }
    public class DepuracionNotDoneOriginal
        {
            public int Id { get; set; }

            public string? CanalDeIngreso { get; set; }
            public string? EstadoAdmision { get; set; }
            public string? FechaAdmision { get; set; }
            public string? TipoDeCuenta { get; set; }
            public string? NoTelefonoPrincipal { get; set; }
            public string? Telefonos { get; set; }
            public string? TipoEMTA { get; set; }
            public string? CtaEspecial { get; set; }
            public string? Hub { get; set; }
            public string? MotivoDeLaOrden { get; set; }
            public string? OrdenDePortabilidad { get; set; }
            public string? Referido { get; set; }
            public string? MotivoDeLaCancelacion { get; set; }
            public string? Sistema { get; set; }
            public string? SubEstado { get; set; }
            public string? NoVTS { get; set; }
            public string? ClaveVendedor { get; set; }
            public string? MensualidadTotal { get; set; }
            public string? TotalDeCNR { get; set; }
            public string? DocumentoDePrueba { get; set; }
            public string? EstadoEnFecha { get; set; }
            public string? CodigoDeTipoDeOrden { get; set; }
            public string? Revision { get; set; }
            public string? CuentaDeFacturacion { get; set; }
            public string? TransferidoAlLibroDeTrabajoDeTransacciones { get; set; }
            public string? Equipo { get; set; }
            public string? FechaDeLaOrden { get; set; }
            public string? Activo { get; set; }
            public string? AplicaTablet { get; set; }
            public string? ConfirmacionDeInstalacion { get; set; }
            public string? NumeroDeOrden { get; set; }
            public string? ClaveDelTecnicoPrincipal { get; set; }
            public string? Tipo { get; set; }
            public string? EstadoDeAsignacionDeCredito { get; set; }
            public string? NoProgramaciones { get; set; }
            public string? Estado { get; set; }
            public string? Compania { get; set; }
            public string? Centro { get; set; }
            public string? ListaDeImpuestos { get; set; }
            public string? Direccion { get; set; }
            public string? Apellidos { get; set; }
            public string? Nombre { get; set; }
            public string? Prioridad { get; set; }
            public string? NumeroDeCuenta { get; set; }
            public string? Aprobado { get; set; }
            public string? AprobadoPor { get; set; }
            public string? Moneda { get; set; }
            public string? ListaDePrecios { get; set; }
            public string? PorcentajeDeDescuento { get; set; }
            public string? CompletadaPor { get; set; }
            public string? MotivoDeReprogramacion { get; set; }
            public string? EnviadaPor { get; set; }
            public string? UltimaModificacionPor { get; set; }
            public string? UltimaModificacion { get; set; }
            public string? Comentarios { get; set; }
            public string? CreadoPor { get; set; }
            public DateTime? Creado { get; set; }
            public DateTime? FechaSolicitada { get; set; }
        }

    public class ArchivosNotdone
        {
            public int Id { get; set; }
            public string? NombreArchivo { get; set; }
            public string? TipoArchivo { get; set; }
            public DateTime? FechaCaptura { get; set; }
            public string? Archivo { get; set; }
            public string? Cve_usuario { get; set; }
            public string? Status { get; set; }
        }


}
