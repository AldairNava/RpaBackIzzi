namespace WebApplication1.Models
{
    public class Retencion
    {
        public int Id { get; set; }
        public string? NumeroAjuste { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? Cve_usuario { get; set; }
        public string? Ip { get; set; }
        public string? Cuenta { get; set; }
        public string? CasoNegocio { get; set; }
        public string? Proceso { get; set; }
        public string? Equipo { get; set; }
        public string? MontoAjustado { get; set; }
        public string? AjustePrevio { get; set; }
    }
}
