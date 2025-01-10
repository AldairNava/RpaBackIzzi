using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class EjecucionExtraccion
    {
        public int Id { get; set; }
        public string? tipoExtraccion { get; set; }
        public DateTime? FechaExtraccion { get; set; }
        public string? ParametrosExtraccion { get; set; }
        public string? Archivo { get; set; }
        public string? Cve_usuario { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? Procesando { get; set; }
        public string? IP { get; set; }
    }

    public class EjecucionExtraccionAutomatizados
    {
        public int Id { get; set; }
        public string? tipoExtraccion { get; set; }
        public DateTime? FechaExtraccion { get; set; }
        public string? ParametrosExtraccion { get; set; }
        public string? Archivo { get; set; }
        public string? Cve_usuario { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? Procesando { get; set; }
        public string? IP { get; set; }
        public string? horaProgramacion { get; set; }
    }

    public class EjecucionExtraccionAutomatizados2
    {
        public int Id { get; set; }
        public string? tipoExtraccion { get; set; }
        public string? FechaExtraccion { get; set; }
        public string? ParametrosExtraccion { get; set; }
        public string? Archivo { get; set; }
        public string? Cve_usuario { get; set; }
        public string? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? horaProgramacion { get; set; }

    }

    public class EjecucionExtraccionAutomatizadosPrueba
    {
        public int Id { get; set; }
        public string? tipoExtraccion { get; set; }
        public DateTime? FechaExtraccion { get; set; }

        public string? ParametrosExtraccion { get; set; }
        public string? Archivo { get; set; }
        public string? Cve_usuario { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? Procesando { get; set; }
        public string? IP { get; set; }
        public string? horaProgramacion { get; set; }
        public string? nombreCron { get; set; }
        public string? scheduleExpression { get; set; }
        public string? tipoProgramacion { get; set; }
        public string? correo { get; set; }
        public string? medioExtraccion { get; set; }

    }

    public class EjecucionExtraccionAutomatizados2Prueba
    {
        public int Id { get; set; }
        public string? tipoExtraccion { get; set; }
        public string? FechaExtraccion { get; set; }
        public string? ParametrosExtraccion { get; set; }
        public string? Archivo { get; set; }
        public string? Cve_usuario { get; set; }
        public string? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? Procesando { get; set; }
        public string? IP { get; set; }
        public string? horaProgramacion { get; set; }
        public string? nombreCron { get; set; }
        public string? scheduleExpression { get; set; }
        public string? tipoProgramacion { get; set; }
        public string? correo { get; set; }
        public string? medioExtraccion { get; set; }
        public int? proceso { get; set; }



    }


}
