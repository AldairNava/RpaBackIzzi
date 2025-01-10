namespace WebApplication1.Models
{
    public class EjecucionReporteModel
    {
        public int Id { get; set; }
        public string? Cve_usuario { get; set; }
        public string? list_id { get; set; }
        public string? Archivo { get; set; }
        public string? Procesando { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Status { get; set; }
        public string? IP { get; set; }
        public string? list_name { get; set; }
    }

    public class cat_extraccionesAutomatizadasModel
    {
        public int Id { get; set; }
        public string? categoria { get; set; }
        public string? motivo { get; set; }
        public string? submotivo { get; set; }
        public string? solucion { get; set; }
        public string? motivosCliente { get; set; }


    }

    public class cat_extraccionesAutomatizadasOSActModel
    {
        public int Id { get; set; }
        public string? tipoOrdenOS { get; set; }
        public string? estadoOS { get; set; }
        public string?  tipoAct { get; set; }
        public string? areaConocimientoAct { get; set; }
        public string? estadoAct { get; set; }


    }
}
