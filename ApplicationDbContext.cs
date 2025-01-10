using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;
using WebApplication1.Models;


namespace WebApplication1
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<EjecucionReporteModel> EjecucionReporte { get; set; } 
        public DbSet<EjecucionExtraccion> EjecucionExtraccion { get; set; } 
        public DbSet<EjecucionDepuracionModel> EjecucionDepuracion { get; set; }
        public DbSet<EjecucionBasesCCModel> BasesCC { get; set; }
        public DbSet<BasesDepuracion> BasesDepuracion { get; set; } 
        public DbSet<HorariosReporte> HorariosReporte { get; set; } 
        public DbSet<depuracionFallas> DepuracionFallas { get; set; } 
        public DbSet<depuracionExportSiebel> DepuracionExportSiebel { get; set; }
        public DbSet<depuracionExportSiebelCanceladas> depuracionExportSiebelCanceladas { get; set; }
        public DbSet<depuracionExportSiebelReserva> depuracionExportSiebelReserva { get; set; }
        public DbSet<depuracionExportSiebel1> DepuracionExportSiebel1 { get; set; }
        public DbSet<depuracionExportSiebelCanceladas1> depuracionExportSiebelCanceladas1 { get; set; }
        public DbSet<depuracionExportSiebelReserva1> depuracionExportSiebelReserva1 { get; set; }
        public DbSet<ReportesModel> Reportes { get; set; }
        public DbSet<AjustesCambiosServiciosModel> AjustesCambioServicios { get; set; }
        public DbSet<EjecucionDepuracionBasesCanceladasOs> DepuracionBasesCanceladasOs { get; set; }
        public DbSet<EjecucionDepuracionBasesCanceladasOsEXT> DepuracionBasesCanceladasOsExt { get; set; }
        public DbSet<NotDoneAjustesModel> AjustesBasesCasosNeogcioCobranza { get; set; }
        public DbSet<NotDoneModel> EjecucionNotDone { get; set; }
        public DbSet<BotsModel> BotsProcess { get; set; }
        public DbSet<BotsModellimpieza> BotsProcessLimpieza { get; set; }
        public DbSet<catalogoProcesosBotsModel> cat_procesos { get; set; }
        public DbSet<catalogoProcesosBotsLimpiezaModel> cat_procesosLimpieza { get; set; }
        public DbSet<AjustesTiempoAjusteModel> AjustesTiempoAjuste { get; set; }
        public DbSet<AjustesNotDoneModel> AjustesNotDone { get; set; }
        public DbSet<AjustesSinValidacionModel> AjustesSinValidacion { get; set; }
        public DbSet<PrefijosRegionDepuracion> PrefijosRegionDepuracion { get; set; }
        public DbSet<CancelacionSinValidacionModel> CancelacionSinValidacion { get; set; }
        public DbSet<CasosNegocioSinValidacionModel> CasosNegocioSinValidacion { get; set; }
        public DbSet<MigracionesLinealesModel> MigracionesLinealeS { get; set; }
        public DbSet<EjecucionExtraccionAutomatizados> EjecucionExtraccionAutomatizados { get; set; }
        public DbSet<EjecucionExtraccionAutomatizados2> EjecucionExtraccionAutomatizados2 { get; set; }
        public DbSet<EjecucionExtraccionAutomatizadosPrueba> EjecucionExtraccionAutomatizadosPrueba { get; set; }
        public DbSet<EjecucionExtraccionAutomatizados2Prueba> EjecucionExtraccionAutomatizados2Prueba { get; set; }
        public DbSet<cat_extraccionesAutomatizadasModel> cat_extraccionesAutomatizadasModel { get; set; }
        public DbSet<cat_extraccionesAutomatizadasOSActModel> cat_extraccionesAutomatizadasOSActModel { get; set; }
        public DbSet<NotDoneCreacionOrdenModel> NotDoneCreacionOrdenModel { get; set; }
        public DbSet<EstadisticasModel> EstadisticasModel { get; set; }
        public DbSet<Hobs> Hobsmodel { get; set; }
        public DbSet<falladepuracion> FallasDepuracion { get; set; }
        public DbSet<series> Series { get; set; }
        public DbSet<EstadisticasAjustesConValidacion> EstadisticasAjustesConValidacion { get; set; }
        public DbSet<EstadisticasNotDonemodel> EstadisticasNotDonestats { get; set; }
        public DbSet<ordenTroubleCall> OrdenTroubleCall { get; set; }
        public DbSet<Depuracion_resultados_importados_RPA> Depuracion_resultados_importados_RPA { get; set; }
        public DbSet<SeriesMasivo>SeriesMasivo { get; set; }
        public DbSet<seriesExlcucion> seriesExlcucion { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NotDoneModel>()
                .HasIndex(b => b.Status);

            modelBuilder.Entity<NotDoneModel>()
                .HasIndex(b => b.clasificacionOrden);

            modelBuilder.Entity<NotDoneModel>()
                .HasIndex(b => b.tipoOrden);

            modelBuilder.Entity<EstadisticasModel>().HasNoKey();
            modelBuilder.Entity<EstadisticasAjustesConValidacion>().HasNoKey();
            modelBuilder.Entity<EstadisticasNotDonemodel>().HasNoKey();


        }

    }
}
