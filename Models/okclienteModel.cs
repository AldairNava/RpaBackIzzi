namespace WebApplication1.Models
{
    public class okClienteReferencia
    {
        public int Id { get; set; }
        public string? Cuenta { get; set; }
        public DateTime? FechaCaptura { get; set; }
    }

    public class okCliente2
    {
        public int Id { get; set; }
        public string? cuenta { get; set; }
        public string? orden { get; set; }
        public string? hub { get; set; }
        public string? nombre { get; set; }
        public string? telefono { get; set; }
        public string? tipo { get; set; }
        public string? comentario { get; set; }
        public string? origen { get; set; }
        public string? encuesta { get; set; }
        public string? osGenerada { get; set; }
        public string? status { get; set; }
        public string? Ip { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? Usuario_Captura { get; set; }

    }
}
