﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230403184405_version_1.1.6")]
    partial class version116
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.BasesDepuracion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Archivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cantidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Fecha_Registro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Horario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BasesDepuracion");
                });

            modelBuilder.Entity("WebApplication1.Models.EjecucionDepuracionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Archivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cve_usuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaCompletado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaExtraccion")
                        .HasColumnType("datetime2");

                    b.Property<string>("HorarioInicio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Procesando")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipoExtraccion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EjecucionDepuracion");
                });

            modelBuilder.Entity("WebApplication1.Models.EjecucionExtraccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Archivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cve_usuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaCompletado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaExtraccion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaInicial")
                        .HasColumnType("datetime2");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParametrosExtraccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Procesando")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipoExtraccion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EjecucionExtraccion");
                });

            modelBuilder.Entity("WebApplication1.Models.EjecucionReporteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Archivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cve_usuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaCaptura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaCompletado")
                        .HasColumnType("datetime2");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Procesando")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("list_id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EjecucionReporte");
                });

            modelBuilder.Entity("WebApplication1.Models.HorariosReporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Horario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HorariosReporte");
                });

            modelBuilder.Entity("WebApplication1.Models.depuracionExportSiebel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Activo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AplicaTablet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Aprobado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AprobadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CICPotencial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CMDSCER")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CMUSCER")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CVTipoConexion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cerrado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaveTecnicoPrincipal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaveVendedor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoEscenario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoTipoOrden")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comentarios")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompletadaPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmacionInstalacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Creado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CtaEspecial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CuentaFacturacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CuentaPrepago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnviadaPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Equipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoAdmision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoAsignacionCredito")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstatusActividades")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Evento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FallaGeneralAsociada")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaAdmision")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaOrden")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaOriginal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaSolicitada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Hub")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstalacionExpress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListaPrecios")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MensalidadTotal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotivoCancelacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotivoOrden")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotivoReprogramacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nodo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumOrden")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumProgramaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroCuenta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroVTS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrdenPedido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrdenPortabilidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prioridad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PuntosTecnicos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RPT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RXDS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Referido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Revision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SNRDS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SNRUP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Segmento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sistema")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubEstado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubMotivoOrden")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sucursal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TXUP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoCuenta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoCuentaPrepago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoEmta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalCNR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransferidoLibroTrabajoTransacciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UltimaModificacionPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UltimaModificion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VelocidadPrepago")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DepuracionExportSiebel");
                });

            modelBuilder.Entity("WebApplication1.Models.depuracionFallas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Categoria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaCarga")
                        .HasColumnType("datetime2");

                    b.Property<string>("FechaInicio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiberDeep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hub")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Inicidente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Motivos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nodo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroFG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Semaforo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Solucion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Submotivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tecnologia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vencimiento")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DepuracionFallas");
                });
#pragma warning restore 612, 618
        }
    }
}
