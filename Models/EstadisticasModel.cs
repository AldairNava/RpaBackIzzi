using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    //esto es para el de depuracion cc y ext
    public class EstadisticasModel
    {
        [NotMapped]
        public int Id { get; set; }
        [Column("Falla RPA")]
        public int? fallaRPA { get; set; }
        [Column("Error Operativo")]
        public int? errorOperativo { get; set; }
        [Column("Registro pendiente")]
        public int? registroPendiente { get; set; }
        [Column("Registro exitoso")]
        public int? registroExitoso { get; set; }
        [Column("Orden cancelada")]
        public int? ordenCancelada { get; set; }
        [Column("Total")]
        public int? total { get; set; }

        [Column("base")]
        public string? Base { get; set; }

    }

    public class EstadisticasAjustesConValidacion
    {
        [NotMapped]
        public int Id { get; set; }
        [Column("Ajuste realizado")]
        public int? ajusteRealizado { get; set; }
        [Column("Error Operativo")]
        public int? errorOperativo { get; set; }
        [Column("Registro pendiente")]
        public int? registroPendiente { get; set; }
        [Column("Inconsistencia en Siebel")]
        public int? inconcistenciaSiebel { get; set; }
        [Column("Falla RPA")]
        public int? fallaRPA { get; set; }
        [Column("Total")]
        public int? total { get; set; }

        [Column("base")]
        public string? Base { get; set; }

    }

    public class EstadisticasNotDonemodel
    {
        [NotMapped]
        public int Id { get; set; }
        [Column("Completado")]
        public int? ajusteRealizado { get; set; }
        [Column("Error Operativo")]
        public int? errorOperativo { get; set; }
        [Column("Registro pendiente")]
        public int? registroPendiente { get; set; }
        [Column("Inconsistencia en Siebel")]
        public int? inconcistenciaSiebel { get; set; }
        [Column("Falla RPA")]
        public int? fallaRPA { get; set; }
        [Column("Total")]
        public int? total { get; set; }
        [Column("base")]
        public string? Base { get; set; }

    }



}
