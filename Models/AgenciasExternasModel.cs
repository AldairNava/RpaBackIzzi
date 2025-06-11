using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CreacionCNs
    {
        public int Id { get; set; }
        public string? CnGenerado { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? Cve_usuario { get; set; }
        public string? Ip { get; set; }
        public string? Cuenta { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string? Categoria { get; set; }
        public string? Mootivo { get; set; }
        public string? SubMotivo { get; set; }
        public string? Solucion { get; set; }
        public string? SaldoIncobrable { get; set; }
        public string? Promocion { get; set; }
        public string? Ajuste { get; set; }
        public DateTime? FechaGestion { get; set; }
        public string? Tipo { get; set; }
    }
}
