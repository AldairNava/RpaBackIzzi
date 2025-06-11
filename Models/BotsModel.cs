using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BotsModel
    {
        public int Id { get; set; }
        public string? comentarios { get; set; }
        public string? hostName { get; set; }
        public string? ip { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public DateTime? created_at { get; set; }
        public int? ProcesoBotId { get; set; }
        public catalogoProcesosBotsModel? ProcesoBot { get; set; }
        public string? estado { get; set; }

    }
    public class BotsModellimpieza
    {
        public int Id { get; set; }
        public string? comentarios { get; set; }
        public string? hostName { get; set; }
        public string? ip { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public DateTime? created_at { get; set; }
        public int? ProcesoBotId { get; set; }
        public catalogoProcesosBotsLimpiezaModel? ProcesoBot { get; set; }
        public string? estado { get; set; }

    }
    public class BotsModelRetencion
    {
        public int Id { get; set; }
        public string? comentarios { get; set; }
        public string? hostName { get; set; }
        public string? ip { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public DateTime? created_at { get; set; }
        public int? ProcesoBotId { get; set; }
        public catalogoProcesosBotsRetencionaModel? ProcesoBot { get; set; }
        public string? estado { get; set; }

    }

    public class catalogoProcesosBotsModel
    {
        public int Id { get; set; }
        public string? Name_process { get; set; }
        public string? usuario { get; set; }
        public string? password { get; set; }
        public DateTime? update_At { get; set; }
        public string? Status { get; set; }
        public string? ulitmoDiaSend { get; set; }
        public string? ultimo_usuario { get; set; }

    }
    public class catalogoProcesosBotsLimpiezaModel
    {
        public int Id { get; set; }
        public string? Name_process { get; set; }
        public string? Name_usuario { get; set; }
        public string? usuario { get; set; }
        public string? password { get; set; }
        public DateTime? update_At { get; set; }
        public string? Status { get; set; }
        public string? ulitmoDiaSend { get; set; }

    }
    public class catalogoProcesosBotsRetencionaModel
    {
        public int Id { get; set; }
        public string? Name_process { get; set; }
        public string? Name_usuario { get; set; }
        public string? usuario { get; set; }
        public string? password { get; set; }
        public DateTime? update_At { get; set; }
        public string? Status { get; set; }
        public string? ulitmoDiaSend { get; set; }

    }

    public class Hobs
    {
        public int Id { get; set; }
        public string? hubs { get; set; }
        public string? usuario { get; set; }
        public DateTime? FechaCaptura { get; set; }

    }
    public class falladepuracion
    {
        public int Id { get; set; }
        public string? falla { get; set; }
        public DateTime? FechaCaptura { get; set; }

    }
    public class series
    {
        public int Id { get; set; }
        public string? serie { get; set; }

    }

    public class seriesExlcucion
    {
        public int Id { get; set; }
        public string? serie { get; set; }

    }

    public class SeriesMasivo
    {
        public int Id { get; set; }
        public string? serie { get; set; }
        public string? puntoInventario { get; set; }
        public string? status { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }

    }

    public class MailRequest
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }

    public class Depuracion_resultados_importados_RPA
    {
        public int Id { get; set; }
        public int? record_id { get; set; }
        public string? contact_info { get; set; }
        public string? contact_info_type { get; set; }
        public string? record_type { get; set; }
        public string? record_status { get; set; }
        public string? call_result { get; set; }
        public int? attempt { get; set; }
        public DateTime? dial_sched_time { get; set; }
        public DateTime? call_time { get; set; }
        public DateTime? daily_from { get; set; }
        public string? daily_till { get; set; }
        public int? tz_dbid { get; set; }
        public int? campaign_id { get; set; }
        public string? agent_id { get; set; }
        public int? chain_id { get; set; }
        public int? chain_n { get; set; }
        public int? group_id { get; set; }
        public string? app_id { get; set; }
        public string? treatments { get; set; }
        public string? media_ref { get; set; }
        public string? email_subject { get; set; }
        public int? email_template_id { get; set; }
        public int? switch_id { get; set; }
        public string? Nodo { get; set; }
        public string? Tipo { get; set; }
        public string? Hub { get; set; }
        public string? Opcion_Digitada { get; set; }
        public DateTime? Fecha_de_la_Orden { get; set; }
        public string? Telefonos { get; set; }
        public string? Comentarios { get; set; }
        public string? Estado { get; set; }
        public string? N_de_Cuenta { get; set; }
        public string? N_de_Orden { get; set; }
        public string? Motivo_de_la_Orden { get; set; }
        public DateTime? Fecha_solicitada { get; set; }
        public string? Rama { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public string? Telefono3 { get; set; }
        public string? Telefono4 { get; set; }
        public string? Telefono5 { get; set; }
        public string? Telefono6 { get; set; }
        public string? Compania { get; set; }
    }

    public class ordenTroubleCall
    {
        public int Id { get; set; }
        public string? NumeroOrden { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? Cve_usuario { get; set; }
        public string? Ip { get; set; }
        public string? Procesando { get; set; }
        public string? Cuenta { get; set; }
        public string? Tipo { get; set; }
        public string? Motivo { get; set; }
        public string? Comentarios { get; set; }
        public string? NumeroCN { get; set; }

    }

    public class okcliente
    {
        public int Id { get; set; }
        public string? CnGenerado { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public string? Status { get; set; }
        public string? Cve_usuario { get; set; }
        public string? Ip { get; set; }
        public string? Cuenta { get; set; }
        public string? numeroOrden { get; set; }
        public string? Hub { get; set; }
        public string? TipoOferta { get; set; }
        public string? FechaEncuesta { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }

    }


    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public string Otros { get; set; }
    }

    public class AgenteDepuracion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string cuenta { get; set; }
        public string orden { get; set; }
        public string TipoCN { get; set; }
        public string CNGenrado { get; set; }
        public string status { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public DateTime? FechaCompletado { get; set; }

    }

}
