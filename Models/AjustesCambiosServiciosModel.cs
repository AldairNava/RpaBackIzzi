namespace WebApplication1.Models
{
    public class AjustesCambiosServiciosModel
    {
        public int Id { get; set; }
        public string? cuenta { get; set; }
        public string? casoNegocio { get; set; }
        public string? Cve_usuario { get; set; }
        public string? motivo { get; set; }
        public string? motivoCliente { get; set; }
        public string? estado { get; set; }

        public string? Status { get; set; }
        public string? Procesando { get; set; }
        public string? IP { get; set; }
        public string? FechaCreado { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public DateTime? FechaCarga { get; set; }
        public string? FechaVencimiento { get; set; }
    }

    public class MigracionesLinealesModel
    {
        public int Id { get; set; }
        public string? beneficioOtorgado { get; set; }
        public string? cn { get; set; }
        public string? cuenta { get; set; }
        public string? canal { get; set; }
        public string? fechaBenOtorgada { get; set; }
        public string? fechaEnvioCiber { get; set; }
        public string? fechaRecibidoBO { get; set; }
        public string? fechaCarga { get; set; }
        public string? nombreCliente { get; set; }
        public string? os { get; set; }
        public string? paqueteOrigen { get; set; }
        public string? estatus { get; set; }
        public string? subMotivo { get; set; }
        public string? usuario { get; set; }


        public string? Cve_usuario { get; set; }
        public string? Status { get; set; }
        public string? Procesando { get; set; }
        public string? IP { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }
    }





}
