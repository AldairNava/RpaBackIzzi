namespace WebApplication1.Models
{
    public class EjecucionDepuracionModel
    {
        public int Id { get; set; }
        public string? tipoExtraccion { get; set; }
        public DateTime? FechaExtraccion { get; set; }
        public string? FechaExtraccion1 { get; set; }
        public string? Archivo { get; set; }
        public string? Cve_usuario { get; set; }
        public string? HorarioInicio { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? FechaCompletado1 { get; set; }
        public string? Status { get; set; }
        public string? Procesando { get; set; }
        public string? IP { get; set; }
    }

    public class EjecucionBasesCCModel
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
        public string? TipoArchivo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? Archivo { get; set; }
        public string? Cve_usuario { get; set; }
        public string? HorarioInicio { get; set; }
        public string? Status { get; set; }
    }

    public class BasesDepuracion
    {
        public int Id { get; set; }
        public string? Horario { get; set; }
        public string? Cantidad { get; set; }
        public string? Archivo { get; set; }                
        public DateTime? Fecha_Registro { get; set; }
        public string? Fecha_Registro1 { get; set; }

    }

    public class HorariosReporte
    {
        public int Id { get; set; }
        public string? Horario { get; set; }
        public string? corteInicio { get; set; }
        public string? corteFin { get; set; }

    }

    public class depuracionFallas
    {
        public int Id { get; set; }
        public string? Semaforo { get; set; }
        public string? Vencimiento { get; set; }
        public string? NumeroFG { get; set; }
        public string? Categoria { get; set; }
        public string? Motivos { get; set; }
        public string? Submotivo { get; set; }
        public string? Solucion { get; set; }
        public string? Tecnologia { get; set; }
        public string? Estado { get; set; }
        public string? Hub { get; set; }
        public string? Rama { get; set; }
        public string? Nodo { get; set; }
        public string? FiberDeep { get; set; }
        public string? FechaInicio { get; set; }
        public string? Nombre { get; set; }
        public string? Inicidente { get; set; }
        public string? FechaCarga { get; set; }


    }

    public class depuracionExportSiebel
    {
        public int Id { get; set; }
        public string? CVTipoConexion { get; set; }
        public string? CanalDeIngreso { get; set; }
        public string? CMDSCER { get; set; }
        public string? CMUSCER { get; set; }
        public string? CodigoEscenario { get; set; }
        public string? EstadoAdmision { get; set; }
        public string? FallaGeneralAsociada { get; set; }
        public string? FechaAdmision { get; set; }
        public string? FD { get; set; }
        public string? Nodo { get; set; }
        public string? Rama { get; set; }
        public string? RXDS { get; set; }
        public string? OrdenSOA { get; set; }
        public string? Segmento { get; set; }
        public string? SNRDS { get; set; }
        public string? SNRUP { get; set; }
        public string? OrdenPedido { get; set; }
        public string? PuntosTecnicos { get; set; }
        public string? TipoCuenta { get; set; }
        public string? TXUP { get; set; }
        public string? ÍnstalacionExpress { get; set; }
        public string? TelefonoPrincipal { get; set; }
        public string? Telefonos { get; set; }
        public string? TipoEMTA { get; set; }
        public string? CtaEspecial { get; set; }
        public string? Hub { get; set; }
        public string? MotivoOrden { get; set; }
        public string? Unnamed27 { get; set; }
        public string? OrdenPortabilidad { get; set; }
        public string? Referido { get; set; }
        public string? MotivoCancelacion { get; set; }
        public string? Sistema { get; set; }
        public string? SubEstado { get; set; }
        public string? NoVTS { get; set; }
        public string? ClaveVendedor { get; set; }
        public string? MensalidadTotal { get; set; }
        public string? TotalCNR { get; set; }
        public string? DocumentoPrueba { get; set; }
        public string? EstadoFecha { get; set; }
        public string? CodigoTipoOrden { get; set; }
        public string? Revision { get; set; }
        public string? CuentaFacturacion { get; set; }
        public string? TransferidoTrabajoTransacciones { get; set; }
        public string? Equipo { get; set; }
        public string? FechaOrden { get; set; }
        public string? Activo { get; set; }
        public string? AplicaTablet { get; set; }
        public string? ConfirmacionInstalacion { get; set; }
        public string? NumOrden { get; set; }
        public string? ClaveTecnicoPrincipal { get; set; }
        public string? Tipo { get; set; }
        public string? Tipo1 { get; set; }
        public string? EstadoAsignacionCredito { get; set; }
        public string? Unnamed53 { get; set; }
        public string? NumProgramaciones { get; set; }
        public string? Unnamed55 { get; set; }
        public string? Estado { get; set; }
        public string? Compania { get; set; }
        public string? Centro { get; set; }
        public string? ListaImpuestos { get; set; }
        public string? Direccion { get; set; }
        public string? Apellidos { get; set; }
        public string? Nombre { get; set; }
        public string? Prioridad { get; set; }
        public string? NumCuenta { get; set; }
        public string? Aprobado { get; set; }
        public string? AprobadoPor { get; set; }
        public string? Moneda { get; set; }
        public string? ListaPrecios { get; set; }
        public string? PorcientoDescuento { get; set; }
        public string? CompletadaPor { get; set; }
        public string? MotivoReprogramacion { get; set; }
        public string? EnviadaPor { get; set; }
        public string? UltimaModificacionPor { get; set; }
        public string? UltimaModificacion { get; set; }
        public string? Comentarios { get; set; }
        public string? CreadoPor { get; set; }
        public string? Creado { get; set; }
        public string? Evento { get; set; }
        public string? FechaSolicitada { get; set; }
        public string? Organizacion { get; set; }
        public string? Cerrado { get; set; }
        public string? Unnamed82 { get; set; }
        public string? CICPotencial { get; set; }

    }

    public class depuracionExportSiebelCanceladas
    {
        public int Id { get; set; }
        public string? CVTipoConexion { get; set; }
        public string? CanalDeIngreso { get; set; }
        public string? CMDSCER { get; set; }
        public string? CMUSCER { get; set; }
        public string? CodigoEscenario { get; set; }
        public string? EstadoAdmision { get; set; }
        public string? FallaGeneralAsociada { get; set; }
        public string? FechaAdmision { get; set; }
        public string? FD { get; set; }
        public string? Nodo { get; set; }
        public string? Rama { get; set; }
        public string? RXDS { get; set; }
        public string? OrdenSOA { get; set; }
        public string? Segmento { get; set; }
        public string? SNRDS { get; set; }
        public string? SNRUP { get; set; }
        public string? OrdenPedido { get; set; }
        public string? PuntosTecnicos { get; set; }
        public string? TipoCuenta { get; set; }
        public string? TXUP { get; set; }
        public string? ÍnstalacionExpress { get; set; }
        public string? TelefonoPrincipal { get; set; }
        public string? Telefonos { get; set; }
        public string? TipoEMTA { get; set; }
        public string? CtaEspecial { get; set; }
        public string? Hub { get; set; }
        public string? MotivoOrden { get; set; }
        public string? Unnamed27 { get; set; }
        public string? OrdenPortabilidad { get; set; }
        public string? Referido { get; set; }
        public string? MotivoCancelacion { get; set; }
        public string? Sistema { get; set; }
        public string? SubEstado { get; set; }
        public string? NoVTS { get; set; }
        public string? ClaveVendedor { get; set; }
        public string? MensalidadTotal { get; set; }
        public string? TotalCNR { get; set; }
        public string? DocumentoPrueba { get; set; }
        public string? EstadoFecha { get; set; }
        public string? CodigoTipoOrden { get; set; }
        public string? Revision { get; set; }
        public string? CuentaFacturacion { get; set; }
        public string? TransferidoTrabajoTransacciones { get; set; }
        public string? Equipo { get; set; }
        public string? FechaOrden { get; set; }
        public string? Activo { get; set; }
        public string? AplicaTablet { get; set; }
        public string? ConfirmacionInstalacion { get; set; }
        public string? NumOrden { get; set; }
        public string? ClaveTecnicoPrincipal { get; set; }
        public string? Tipo { get; set; }
        public string? Tipo1 { get; set; }
        public string? EstadoAsignacionCredito { get; set; }
        public string? Unnamed53 { get; set; }
        public string? NumProgramaciones { get; set; }
        public string? Unnamed55 { get; set; }
        public string? Estado { get; set; }
        public string? Compania { get; set; }
        public string? Centro { get; set; }
        public string? ListaImpuestos { get; set; }
        public string? Direccion { get; set; }
        public string? Apellidos { get; set; }
        public string? Nombre { get; set; }
        public string? Prioridad { get; set; }
        public string? NumCuenta { get; set; }
        public string? Aprobado { get; set; }
        public string? AprobadoPor { get; set; }
        public string? Moneda { get; set; }
        public string? ListaPrecios { get; set; }
        public string? PorcientoDescuento { get; set; }
        public string? CompletadaPor { get; set; }
        public string? MotivoReprogramacion { get; set; }
        public string? EnviadaPor { get; set; }
        public string? UltimaModificacionPor { get; set; }
        public string? UltimaModificacion { get; set; }
        public string? Comentarios { get; set; }
        public string? CreadoPor { get; set; }
        public string? Creado { get; set; }
        public string? Evento { get; set; }
        public string? FechaSolicitada { get; set; }
        public string? Organizacion { get; set; }
        public string? Cerrado { get; set; }
        public string? Unnamed82 { get; set; }
        public string? CICPotencial { get; set; }
    }

    public class depuracionExportSiebelReserva
    {
        public int Id { get; set; }
        public string? CVTipoConexion { get; set; }
        public string? CanalDeIngreso { get; set; }
        public string? CMDSCER { get; set; }
        public string? CMUSCER { get; set; }
        public string? CodigoEscenario { get; set; }
        public string? EstadoAdmision { get; set; }
        public string? FallaGeneralAsociada { get; set; }
        public string? FechaAdmision { get; set; }
        public string? FD { get; set; }
        public string? Nodo { get; set; }
        public string? Rama { get; set; }
        public string? RXDS { get; set; }
        public string? OrdenSOA { get; set; }
        public string? Segmento { get; set; }
        public string? SNRDS { get; set; }
        public string? SNRUP { get; set; }
        public string? OrdenPedido { get; set; }
        public string? PuntosTecnicos { get; set; }
        public string? TipoCuenta { get; set; }
        public string? TXUP { get; set; }
        public string? ÍnstalacionExpress { get; set; }
        public string? TelefonoPrincipal { get; set; }
        public string? Telefonos { get; set; }
        public string? TipoEMTA { get; set; }
        public string? CtaEspecial { get; set; }
        public string? Hub { get; set; }
        public string? MotivoOrden { get; set; }
        public string? Unnamed27 { get; set; }
        public string? OrdenPortabilidad { get; set; }
        public string? Referido { get; set; }
        public string? MotivoCancelacion { get; set; }
        public string? Sistema { get; set; }
        public string? SubEstado { get; set; }
        public string? NoVTS { get; set; }
        public string? ClaveVendedor { get; set; }
        public string? MensalidadTotal { get; set; }
        public string? TotalCNR { get; set; }
        public string? DocumentoPrueba { get; set; }
        public string? EstadoFecha { get; set; }
        public string? CodigoTipoOrden { get; set; }
        public string? Revision { get; set; }
        public string? CuentaFacturacion { get; set; }
        public string? TransferidoTrabajoTransacciones { get; set; }
        public string? Equipo { get; set; }
        public string? FechaOrden { get; set; }
        public string? Activo { get; set; }
        public string? AplicaTablet { get; set; }
        public string? ConfirmacionInstalacion { get; set; }
        public string? NumOrden { get; set; }
        public string? ClaveTecnicoPrincipal { get; set; }
        public string? Tipo { get; set; }
        public string? Tipo1 { get; set; }
        public string? EstadoAsignacionCredito { get; set; }
        public string? Unnamed53 { get; set; }
        public string? NumProgramaciones { get; set; }
        public string? Unnamed55 { get; set; }
        public string? Estado { get; set; }
        public string? Compania { get; set; }
        public string? Centro { get; set; }
        public string? ListaImpuestos { get; set; }
        public string? Direccion { get; set; }
        public string? Apellidos { get; set; }
        public string? Nombre { get; set; }
        public string? Prioridad { get; set; }
        public string? NumCuenta { get; set; }
        public string? Aprobado { get; set; }
        public string? AprobadoPor { get; set; }
        public string? Moneda { get; set; }
        public string? ListaPrecios { get; set; }
        public string? PorcientoDescuento { get; set; }
        public string? CompletadaPor { get; set; }
        public string? MotivoReprogramacion { get; set; }
        public string? EnviadaPor { get; set; }
        public string? UltimaModificacionPor { get; set; }
        public string? UltimaModificacion { get; set; }
        public string? Comentarios { get; set; }
        public string? CreadoPor { get; set; }
        public string? Creado { get; set; }
        public string? Evento { get; set; }
        public string? FechaSolicitada { get; set; }
        public string? Organizacion { get; set; }
        public string? Cerrado { get; set; }
        public string? Unnamed82 { get; set; }
        public string? CICPotencial { get; set; }
    }

    public class depuracionExportSiebel1
    {
        public int Id { get; set; }
        public string? CVTipoConexion { get; set; }
        public string? CanalDeIngreso { get; set; }
        public string? CMDSCER { get; set; }
        public string? CMUSCER { get; set; }
        public string? CodigoEscenario { get; set; }
        public string? EstadoAdmision { get; set; }
        public string? FallaGeneralAsociada { get; set; }
        public string? FechaAdmision { get; set; }
        public string? FD { get; set; }
        public string? Nodo { get; set; }
        public string? Rama { get; set; }
        public string? RXDS { get; set; }
        public string? OrdenSOA { get; set; }
        public string? Segmento { get; set; }
        public string? SNRDS { get; set; }
        public string? SNRUP { get; set; }
        public string? OrdenPedido { get; set; }
        public string? PuntosTecnicos { get; set; }
        public string? TipoCuenta { get; set; }
        public string? TXUP { get; set; }
        public string? ÍnstalacionExpress { get; set; }
        public string? TelefonoPrincipal { get; set; }
        public string? Telefonos { get; set; }
        public string? TipoEMTA { get; set; }
        public string? CtaEspecial { get; set; }
        public string? Hub { get; set; }
        public string? MotivoOrden { get; set; }
        public string? Unnamed27 { get; set; }
        public string? OrdenPortabilidad { get; set; }
        public string? Referido { get; set; }
        public string? MotivoCancelacion { get; set; }
        public string? Sistema { get; set; }
        public string? SubEstado { get; set; }
        public string? NoVTS { get; set; }
        public string? ClaveVendedor { get; set; }
        public string? MensalidadTotal { get; set; }
        public string? TotalCNR { get; set; }
        public string? DocumentoPrueba { get; set; }
        public string? EstadoFecha { get; set; }
        public string? CodigoTipoOrden { get; set; }
        public string? Revision { get; set; }
        public string? CuentaFacturacion { get; set; }
        public string? TransferidoTrabajoTransacciones { get; set; }
        public string? Equipo { get; set; }
        public string? FechaOrden { get; set; }
        public string? Activo { get; set; }
        public string? AplicaTablet { get; set; }
        public string? ConfirmacionInstalacion { get; set; }
        public string? NumOrden { get; set; }
        public string? ClaveTecnicoPrincipal { get; set; }
        public string? Tipo { get; set; }
        public string? Tipo1 { get; set; }
        public string? EstadoAsignacionCredito { get; set; }
        public string? Unnamed53 { get; set; }
        public string? NumProgramaciones { get; set; }
        public string? Unnamed55 { get; set; }
        public string? Estado { get; set; }
        public string? Compania { get; set; }
        public string? Centro { get; set; }
        public string? ListaImpuestos { get; set; }
        public string? Direccion { get; set; }
        public string? Apellidos { get; set; }
        public string? Nombre { get; set; }
        public string? Prioridad { get; set; }
        public string? NumCuenta { get; set; }
        public string? Aprobado { get; set; }
        public string? AprobadoPor { get; set; }
        public string? Moneda { get; set; }
        public string? ListaPrecios { get; set; }
        public string? PorcientoDescuento { get; set; }
        public string? CompletadaPor { get; set; }
        public string? MotivoReprogramacion { get; set; }
        public string? EnviadaPor { get; set; }
        public string? UltimaModificacionPor { get; set; }
        public string? UltimaModificacion { get; set; }
        public string? Comentarios { get; set; }
        public string? CreadoPor { get; set; }
        public string? Creado { get; set; }
        public string? Evento { get; set; }
        public string? FechaSolicitada { get; set; }
        public string? Organizacion { get; set; }
        public string? Cerrado { get; set; }
        public string? Unnamed82 { get; set; }
        public string? CICPotencial { get; set; }

    }

    public class depuracionExportSiebelCanceladas1
    {
        public int Id { get; set; }
        public string? CVTipoConexion { get; set; }
        public string? CanalDeIngreso { get; set; }
        public string? CMDSCER { get; set; }
        public string? CMUSCER { get; set; }
        public string? CodigoEscenario { get; set; }
        public string? EstadoAdmision { get; set; }
        public string? FallaGeneralAsociada { get; set; }
        public string? FechaAdmision { get; set; }
        public string? FD { get; set; }
        public string? Nodo { get; set; }
        public string? Rama { get; set; }
        public string? RXDS { get; set; }
        public string? OrdenSOA { get; set; }
        public string? Segmento { get; set; }
        public string? SNRDS { get; set; }
        public string? SNRUP { get; set; }
        public string? OrdenPedido { get; set; }
        public string? PuntosTecnicos { get; set; }
        public string? TipoCuenta { get; set; }
        public string? TXUP { get; set; }
        public string? ÍnstalacionExpress { get; set; }
        public string? TelefonoPrincipal { get; set; }
        public string? Telefonos { get; set; }
        public string? TipoEMTA { get; set; }
        public string? CtaEspecial { get; set; }
        public string? Hub { get; set; }
        public string? MotivoOrden { get; set; }
        public string? Unnamed27 { get; set; }
        public string? OrdenPortabilidad { get; set; }
        public string? Referido { get; set; }
        public string? MotivoCancelacion { get; set; }
        public string? Sistema { get; set; }
        public string? SubEstado { get; set; }
        public string? NoVTS { get; set; }
        public string? ClaveVendedor { get; set; }
        public string? MensalidadTotal { get; set; }
        public string? TotalCNR { get; set; }
        public string? DocumentoPrueba { get; set; }
        public string? EstadoFecha { get; set; }
        public string? CodigoTipoOrden { get; set; }
        public string? Revision { get; set; }
        public string? CuentaFacturacion { get; set; }
        public string? TransferidoTrabajoTransacciones { get; set; }
        public string? Equipo { get; set; }
        public string? FechaOrden { get; set; }
        public string? Activo { get; set; }
        public string? AplicaTablet { get; set; }
        public string? ConfirmacionInstalacion { get; set; }
        public string? NumOrden { get; set; }
        public string? ClaveTecnicoPrincipal { get; set; }
        public string? Tipo { get; set; }
        public string? Tipo1 { get; set; }
        public string? EstadoAsignacionCredito { get; set; }
        public string? Unnamed53 { get; set; }
        public string? NumProgramaciones { get; set; }
        public string? Unnamed55 { get; set; }
        public string? Estado { get; set; }
        public string? Compania { get; set; }
        public string? Centro { get; set; }
        public string? ListaImpuestos { get; set; }
        public string? Direccion { get; set; }
        public string? Apellidos { get; set; }
        public string? Nombre { get; set; }
        public string? Prioridad { get; set; }
        public string? NumCuenta { get; set; }
        public string? Aprobado { get; set; }
        public string? AprobadoPor { get; set; }
        public string? Moneda { get; set; }
        public string? ListaPrecios { get; set; }
        public string? PorcientoDescuento { get; set; }
        public string? CompletadaPor { get; set; }
        public string? MotivoReprogramacion { get; set; }
        public string? EnviadaPor { get; set; }
        public string? UltimaModificacionPor { get; set; }
        public string? UltimaModificacion { get; set; }
        public string? Comentarios { get; set; }
        public string? CreadoPor { get; set; }
        public string? Creado { get; set; }
        public string? Evento { get; set; }
        public string? FechaSolicitada { get; set; }
        public string? Organizacion { get; set; }
        public string? Cerrado { get; set; }
        public string? Unnamed82 { get; set; }
        public string? CICPotencial { get; set; }
    }

    public class depuracionExportSiebelReserva1
    {
        public int Id { get; set; }
        public string? CVTipoConexion { get; set; }
        public string? CanalDeIngreso { get; set; }
        public string? CMDSCER { get; set; }
        public string? CMUSCER { get; set; }
        public string? CodigoEscenario { get; set; }
        public string? EstadoAdmision { get; set; }
        public string? FallaGeneralAsociada { get; set; }
        public string? FechaAdmision { get; set; }
        public string? FD { get; set; }
        public string? Nodo { get; set; }
        public string? Rama { get; set; }
        public string? RXDS { get; set; }
        public string? OrdenSOA { get; set; }
        public string? Segmento { get; set; }
        public string? SNRDS { get; set; }
        public string? SNRUP { get; set; }
        public string? OrdenPedido { get; set; }
        public string? PuntosTecnicos { get; set; }
        public string? TipoCuenta { get; set; }
        public string? TXUP { get; set; }
        public string? ÍnstalacionExpress { get; set; }
        public string? TelefonoPrincipal { get; set; }
        public string? Telefonos { get; set; }
        public string? TipoEMTA { get; set; }
        public string? CtaEspecial { get; set; }
        public string? Hub { get; set; }
        public string? MotivoOrden { get; set; }
        public string? Unnamed27 { get; set; }
        public string? OrdenPortabilidad { get; set; }
        public string? Referido { get; set; }
        public string? MotivoCancelacion { get; set; }
        public string? Sistema { get; set; }
        public string? SubEstado { get; set; }
        public string? NoVTS { get; set; }
        public string? ClaveVendedor { get; set; }
        public string? MensalidadTotal { get; set; }
        public string? TotalCNR { get; set; }
        public string? DocumentoPrueba { get; set; }
        public string? EstadoFecha { get; set; }
        public string? CodigoTipoOrden { get; set; }
        public string? Revision { get; set; }
        public string? CuentaFacturacion { get; set; }
        public string? TransferidoTrabajoTransacciones { get; set; }
        public string? Equipo { get; set; }
        public string? FechaOrden { get; set; }
        public string? Activo { get; set; }
        public string? AplicaTablet { get; set; }
        public string? ConfirmacionInstalacion { get; set; }
        public string? NumOrden { get; set; }
        public string? ClaveTecnicoPrincipal { get; set; }
        public string? Tipo { get; set; }
        public string? Tipo1 { get; set; }
        public string? EstadoAsignacionCredito { get; set; }
        public string? Unnamed53 { get; set; }
        public string? NumProgramaciones { get; set; }
        public string? Unnamed55 { get; set; }
        public string? Estado { get; set; }
        public string? Compania { get; set; }
        public string? Centro { get; set; }
        public string? ListaImpuestos { get; set; }
        public string? Direccion { get; set; }
        public string? Apellidos { get; set; }
        public string? Nombre { get; set; }
        public string? Prioridad { get; set; }
        public string? NumCuenta { get; set; }
        public string? Aprobado { get; set; }
        public string? AprobadoPor { get; set; }
        public string? Moneda { get; set; }
        public string? ListaPrecios { get; set; }
        public string? PorcientoDescuento { get; set; }
        public string? CompletadaPor { get; set; }
        public string? MotivoReprogramacion { get; set; }
        public string? EnviadaPor { get; set; }
        public string? UltimaModificacionPor { get; set; }
        public string? UltimaModificacion { get; set; }
        public string? Comentarios { get; set; }
        public string? CreadoPor { get; set; }
        public string? Creado { get; set; }
        public string? Evento { get; set; }
        public string? FechaSolicitada { get; set; }
        public string? Organizacion { get; set; }
        public string? Cerrado { get; set; }
        public string? Unnamed82 { get; set; }
        public string? CICPotencial { get; set; }
    }


    public class EjecucionDepuracionBasesCanceladasOs
    {
        public int Id { get; set; }
        public int? lead_id { get; set; }
        public string? Comentarios { get; set; }
        public string? Compania { get; set; }
        public string? Estado { get; set; }
        public string? Hub { get; set; }
        public string? MotivoOrden { get; set; }
        public string? Nodo { get; set; }
        public string? Cuenta { get; set; }
        public string? NumOrden { get; set; }
        public string? Tipo { get; set; }
        public string? Cn_generado { get; set; }
        public DateTime? FechaHoraCierre { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? FechaCarga { get; set; }
        public string? FechaCreado { get; set; }
        public string? Cve_usuario { get; set; }
        public string? Status { get; set; }
        public string? Procesando { get; set; }
        public string? IP { get; set; }
        public string? Source { get; set; }


    }

    public class EjecucionDepuracionBasesCanceladasOsEXT
    {
        public int Id { get; set; }
        public int? lead_id { get; set; }
        public string? Cuenta { get; set; }
        public string? Compania { get; set; }
        public string? NumOrden { get; set; }
        public string? Tipo { get; set; }
        public string? MotivoOrden { get; set; }
        public DateTime? FechaTecnico { get; set; }
        public string? Comentarios { get; set; }
        public string? HUB { get; set; }
        public string? RPT { get; set; }
        public string? Region { get; set; }
        public string? QuienResponde { get; set; }
        public string? Transferir { get; set; }
        public string? NombreContacto { get; set; }
        public string? ComentariosCyber { get; set; }
        public string? Nodo { get; set; }
        public string? Source { get; set; }
        public string? time_carga { get; set; }
        public string? Status { get; set; }
        public DateTime? FechaHoraCierre { get; set; }
        public string? cn_generado { get; set; }
        public string? usuario_creo { get; set; }
        public string? User_registro { get; set; }
        public string? Procesando { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public DateTime? FechaCreado { get; set; }
        public string? Cve_usuario { get; set; }
        public string? IP { get; set; }

    }

    public class PrefijosRegionDepuracion
    {
        public int Id { get; set; }
        public string? region { get; set; }
        public string? subregion { get; set; }
        public string? hub { get; set; }
        public string? plaza { get; set; }
        public string? rpt { get; set; }
        public string? hubOrden { get; set; }
        public string? prefijo { get; set; }
        public string? Cve_usuario { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }





}
