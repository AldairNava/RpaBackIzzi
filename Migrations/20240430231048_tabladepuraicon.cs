using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class tabladepuraicon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depuracion_resultados_importados_RPA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recordid = table.Column<int>(name: "record_id", type: "int", nullable: true),
                    contactinfo = table.Column<string>(name: "contact_info", type: "nvarchar(max)", nullable: true),
                    contactinfotype = table.Column<string>(name: "contact_info_type", type: "nvarchar(max)", nullable: true),
                    recordtype = table.Column<string>(name: "record_type", type: "nvarchar(max)", nullable: true),
                    recordstatus = table.Column<string>(name: "record_status", type: "nvarchar(max)", nullable: true),
                    callresult = table.Column<string>(name: "call_result", type: "nvarchar(max)", nullable: true),
                    attempt = table.Column<int>(type: "int", nullable: true),
                    dialschedtime = table.Column<DateTime>(name: "dial_sched_time", type: "datetime2", nullable: true),
                    calltime = table.Column<DateTime>(name: "call_time", type: "datetime2", nullable: true),
                    dailyfrom = table.Column<DateTime>(name: "daily_from", type: "datetime2", nullable: true),
                    dailytill = table.Column<DateTime>(name: "daily_till", type: "datetime2", nullable: true),
                    tzdbid = table.Column<int>(name: "tz_dbid", type: "int", nullable: true),
                    campaignid = table.Column<int>(name: "campaign_id", type: "int", nullable: true),
                    agentid = table.Column<string>(name: "agent_id", type: "nvarchar(max)", nullable: true),
                    chainid = table.Column<int>(name: "chain_id", type: "int", nullable: true),
                    chainn = table.Column<int>(name: "chain_n", type: "int", nullable: true),
                    groupid = table.Column<int>(name: "group_id", type: "int", nullable: true),
                    appid = table.Column<string>(name: "app_id", type: "nvarchar(max)", nullable: true),
                    treatments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mediaref = table.Column<string>(name: "media_ref", type: "nvarchar(max)", nullable: true),
                    emailsubject = table.Column<string>(name: "email_subject", type: "nvarchar(max)", nullable: true),
                    emailtemplateid = table.Column<int>(name: "email_template_id", type: "int", nullable: true),
                    switchid = table.Column<int>(name: "switch_id", type: "int", nullable: true),
                    Nodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpcionDigitada = table.Column<string>(name: "Opcion_Digitada", type: "nvarchar(max)", nullable: true),
                    FechadelaOrden = table.Column<DateTime>(name: "Fecha_de_la_Orden", type: "datetime2", nullable: true),
                    Telefonos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NdeCuenta = table.Column<string>(name: "N_de_Cuenta", type: "nvarchar(max)", nullable: true),
                    NdeOrden = table.Column<string>(name: "N_de_Orden", type: "nvarchar(max)", nullable: true),
                    MotivodelaOrden = table.Column<string>(name: "Motivo_de_la_Orden", type: "nvarchar(max)", nullable: true),
                    Fechasolicitada = table.Column<DateTime>(name: "Fecha_solicitada", type: "datetime2", nullable: true),
                    Rama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compania = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depuracion_resultados_importados_RPA", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depuracion_resultados_importados_RPA");
        }
    }
}
