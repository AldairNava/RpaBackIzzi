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



    }
