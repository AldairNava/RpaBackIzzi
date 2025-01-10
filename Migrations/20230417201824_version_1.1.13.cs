using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoEmta",
                table: "depuracionExportSiebelReserva",
                newName: "TipoEMTA");

            migrationBuilder.RenameColumn(
                name: "VelocidadPrepago",
                table: "depuracionExportSiebelReserva",
                newName: "ÍnstalacionExpress");

            migrationBuilder.RenameColumn(
                name: "UltimaModificion",
                table: "depuracionExportSiebelReserva",
                newName: "Unnamed82");

            migrationBuilder.RenameColumn(
                name: "TransferidoLibroTrabajoTransacciones",
                table: "depuracionExportSiebelReserva",
                newName: "Unnamed55");

            migrationBuilder.RenameColumn(
                name: "TipoCuentaPrepago",
                table: "depuracionExportSiebelReserva",
                newName: "Unnamed53");

            migrationBuilder.RenameColumn(
                name: "Sucursal",
                table: "depuracionExportSiebelReserva",
                newName: "Unnamed27");

            migrationBuilder.RenameColumn(
                name: "SubMotivoOrden",
                table: "depuracionExportSiebelReserva",
                newName: "UltimaModificacion");

            migrationBuilder.RenameColumn(
                name: "RPT",
                table: "depuracionExportSiebelReserva",
                newName: "TransferidoTrabajoTransacciones");

            migrationBuilder.RenameColumn(
                name: "NumeroVTS",
                table: "depuracionExportSiebelReserva",
                newName: "Tipo1");

            migrationBuilder.RenameColumn(
                name: "NumeroCuenta",
                table: "depuracionExportSiebelReserva",
                newName: "Telefonos");

            migrationBuilder.RenameColumn(
                name: "InstalacionExpress",
                table: "depuracionExportSiebelReserva",
                newName: "TelefonoPrincipal");

            migrationBuilder.RenameColumn(
                name: "FechaOriginal",
                table: "depuracionExportSiebelReserva",
                newName: "PorcientoDescuento");

            migrationBuilder.RenameColumn(
                name: "EstatusActividades",
                table: "depuracionExportSiebelReserva",
                newName: "Organizacion");

            migrationBuilder.RenameColumn(
                name: "CuentaPrepago",
                table: "depuracionExportSiebelReserva",
                newName: "OrdenSOA");

            migrationBuilder.RenameColumn(
                name: "TipoEmta",
                table: "depuracionExportSiebelCanceladas",
                newName: "TipoEMTA");

            migrationBuilder.RenameColumn(
                name: "VelocidadPrepago",
                table: "depuracionExportSiebelCanceladas",
                newName: "ÍnstalacionExpress");

            migrationBuilder.RenameColumn(
                name: "UltimaModificion",
                table: "depuracionExportSiebelCanceladas",
                newName: "Unnamed82");

            migrationBuilder.RenameColumn(
                name: "TransferidoLibroTrabajoTransacciones",
                table: "depuracionExportSiebelCanceladas",
                newName: "Unnamed55");

            migrationBuilder.RenameColumn(
                name: "TipoCuentaPrepago",
                table: "depuracionExportSiebelCanceladas",
                newName: "Unnamed53");

            migrationBuilder.RenameColumn(
                name: "Sucursal",
                table: "depuracionExportSiebelCanceladas",
                newName: "Unnamed27");

            migrationBuilder.RenameColumn(
                name: "SubMotivoOrden",
                table: "depuracionExportSiebelCanceladas",
                newName: "UltimaModificacion");

            migrationBuilder.RenameColumn(
                name: "RPT",
                table: "depuracionExportSiebelCanceladas",
                newName: "TransferidoTrabajoTransacciones");

            migrationBuilder.RenameColumn(
                name: "NumeroVTS",
                table: "depuracionExportSiebelCanceladas",
                newName: "Tipo1");

            migrationBuilder.RenameColumn(
                name: "NumeroCuenta",
                table: "depuracionExportSiebelCanceladas",
                newName: "Telefonos");

            migrationBuilder.RenameColumn(
                name: "InstalacionExpress",
                table: "depuracionExportSiebelCanceladas",
                newName: "TelefonoPrincipal");

            migrationBuilder.RenameColumn(
                name: "FechaOriginal",
                table: "depuracionExportSiebelCanceladas",
                newName: "PorcientoDescuento");

            migrationBuilder.RenameColumn(
                name: "EstatusActividades",
                table: "depuracionExportSiebelCanceladas",
                newName: "Organizacion");

            migrationBuilder.RenameColumn(
                name: "CuentaPrepago",
                table: "depuracionExportSiebelCanceladas",
                newName: "OrdenSOA");

            migrationBuilder.RenameColumn(
                name: "TipoEmta",
                table: "DepuracionExportSiebel",
                newName: "TipoEMTA");

            migrationBuilder.RenameColumn(
                name: "VelocidadPrepago",
                table: "DepuracionExportSiebel",
                newName: "ÍnstalacionExpress");

            migrationBuilder.RenameColumn(
                name: "UltimaModificion",
                table: "DepuracionExportSiebel",
                newName: "Unnamed82");

            migrationBuilder.RenameColumn(
                name: "TransferidoLibroTrabajoTransacciones",
                table: "DepuracionExportSiebel",
                newName: "Unnamed55");

            migrationBuilder.RenameColumn(
                name: "TipoCuentaPrepago",
                table: "DepuracionExportSiebel",
                newName: "Unnamed53");

            migrationBuilder.RenameColumn(
                name: "Sucursal",
                table: "DepuracionExportSiebel",
                newName: "Unnamed27");

            migrationBuilder.RenameColumn(
                name: "SubMotivoOrden",
                table: "DepuracionExportSiebel",
                newName: "UltimaModificacion");

            migrationBuilder.RenameColumn(
                name: "RPT",
                table: "DepuracionExportSiebel",
                newName: "TransferidoTrabajoTransacciones");

            migrationBuilder.RenameColumn(
                name: "NumeroVTS",
                table: "DepuracionExportSiebel",
                newName: "Tipo1");

            migrationBuilder.RenameColumn(
                name: "NumeroCuenta",
                table: "DepuracionExportSiebel",
                newName: "Telefonos");

            migrationBuilder.RenameColumn(
                name: "InstalacionExpress",
                table: "DepuracionExportSiebel",
                newName: "TelefonoPrincipal");

            migrationBuilder.RenameColumn(
                name: "FechaOriginal",
                table: "DepuracionExportSiebel",
                newName: "PorcientoDescuento");

            migrationBuilder.RenameColumn(
                name: "EstatusActividades",
                table: "DepuracionExportSiebel",
                newName: "Organizacion");

            migrationBuilder.RenameColumn(
                name: "CuentaPrepago",
                table: "DepuracionExportSiebel",
                newName: "OrdenSOA");

            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CanalDeIngreso",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Centro",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Compania",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentoPrueba",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoFecha",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListaImpuestos",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoVTS",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumCuenta",
                table: "depuracionExportSiebelReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CanalDeIngreso",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Centro",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Compania",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentoPrueba",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoFecha",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListaImpuestos",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoVTS",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumCuenta",
                table: "depuracionExportSiebelCanceladas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CanalDeIngreso",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Centro",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Compania",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentoPrueba",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoFecha",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListaImpuestos",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoVTS",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumCuenta",
                table: "DepuracionExportSiebel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "CanalDeIngreso",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "Centro",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "Compania",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "DocumentoPrueba",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "EstadoFecha",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "ListaImpuestos",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "NoVTS",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "NumCuenta",
                table: "depuracionExportSiebelReserva");

            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "CanalDeIngreso",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "Centro",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "Compania",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "DocumentoPrueba",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "EstadoFecha",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "ListaImpuestos",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "NoVTS",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "NumCuenta",
                table: "depuracionExportSiebelCanceladas");

            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "CanalDeIngreso",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "Centro",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "Compania",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "DocumentoPrueba",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "EstadoFecha",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "ListaImpuestos",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "NoVTS",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "DepuracionExportSiebel");

            migrationBuilder.DropColumn(
                name: "NumCuenta",
                table: "DepuracionExportSiebel");

            migrationBuilder.RenameColumn(
                name: "TipoEMTA",
                table: "depuracionExportSiebelReserva",
                newName: "TipoEmta");

            migrationBuilder.RenameColumn(
                name: "ÍnstalacionExpress",
                table: "depuracionExportSiebelReserva",
                newName: "VelocidadPrepago");

            migrationBuilder.RenameColumn(
                name: "Unnamed82",
                table: "depuracionExportSiebelReserva",
                newName: "UltimaModificion");

            migrationBuilder.RenameColumn(
                name: "Unnamed55",
                table: "depuracionExportSiebelReserva",
                newName: "TransferidoLibroTrabajoTransacciones");

            migrationBuilder.RenameColumn(
                name: "Unnamed53",
                table: "depuracionExportSiebelReserva",
                newName: "TipoCuentaPrepago");

            migrationBuilder.RenameColumn(
                name: "Unnamed27",
                table: "depuracionExportSiebelReserva",
                newName: "Sucursal");

            migrationBuilder.RenameColumn(
                name: "UltimaModificacion",
                table: "depuracionExportSiebelReserva",
                newName: "SubMotivoOrden");

            migrationBuilder.RenameColumn(
                name: "TransferidoTrabajoTransacciones",
                table: "depuracionExportSiebelReserva",
                newName: "RPT");

            migrationBuilder.RenameColumn(
                name: "Tipo1",
                table: "depuracionExportSiebelReserva",
                newName: "NumeroVTS");

            migrationBuilder.RenameColumn(
                name: "Telefonos",
                table: "depuracionExportSiebelReserva",
                newName: "NumeroCuenta");

            migrationBuilder.RenameColumn(
                name: "TelefonoPrincipal",
                table: "depuracionExportSiebelReserva",
                newName: "InstalacionExpress");

            migrationBuilder.RenameColumn(
                name: "PorcientoDescuento",
                table: "depuracionExportSiebelReserva",
                newName: "FechaOriginal");

            migrationBuilder.RenameColumn(
                name: "Organizacion",
                table: "depuracionExportSiebelReserva",
                newName: "EstatusActividades");

            migrationBuilder.RenameColumn(
                name: "OrdenSOA",
                table: "depuracionExportSiebelReserva",
                newName: "CuentaPrepago");

            migrationBuilder.RenameColumn(
                name: "TipoEMTA",
                table: "depuracionExportSiebelCanceladas",
                newName: "TipoEmta");

            migrationBuilder.RenameColumn(
                name: "ÍnstalacionExpress",
                table: "depuracionExportSiebelCanceladas",
                newName: "VelocidadPrepago");

            migrationBuilder.RenameColumn(
                name: "Unnamed82",
                table: "depuracionExportSiebelCanceladas",
                newName: "UltimaModificion");

            migrationBuilder.RenameColumn(
                name: "Unnamed55",
                table: "depuracionExportSiebelCanceladas",
                newName: "TransferidoLibroTrabajoTransacciones");

            migrationBuilder.RenameColumn(
                name: "Unnamed53",
                table: "depuracionExportSiebelCanceladas",
                newName: "TipoCuentaPrepago");

            migrationBuilder.RenameColumn(
                name: "Unnamed27",
                table: "depuracionExportSiebelCanceladas",
                newName: "Sucursal");

            migrationBuilder.RenameColumn(
                name: "UltimaModificacion",
                table: "depuracionExportSiebelCanceladas",
                newName: "SubMotivoOrden");

            migrationBuilder.RenameColumn(
                name: "TransferidoTrabajoTransacciones",
                table: "depuracionExportSiebelCanceladas",
                newName: "RPT");

            migrationBuilder.RenameColumn(
                name: "Tipo1",
                table: "depuracionExportSiebelCanceladas",
                newName: "NumeroVTS");

            migrationBuilder.RenameColumn(
                name: "Telefonos",
                table: "depuracionExportSiebelCanceladas",
                newName: "NumeroCuenta");

            migrationBuilder.RenameColumn(
                name: "TelefonoPrincipal",
                table: "depuracionExportSiebelCanceladas",
                newName: "InstalacionExpress");

            migrationBuilder.RenameColumn(
                name: "PorcientoDescuento",
                table: "depuracionExportSiebelCanceladas",
                newName: "FechaOriginal");

            migrationBuilder.RenameColumn(
                name: "Organizacion",
                table: "depuracionExportSiebelCanceladas",
                newName: "EstatusActividades");

            migrationBuilder.RenameColumn(
                name: "OrdenSOA",
                table: "depuracionExportSiebelCanceladas",
                newName: "CuentaPrepago");

            migrationBuilder.RenameColumn(
                name: "TipoEMTA",
                table: "DepuracionExportSiebel",
                newName: "TipoEmta");

            migrationBuilder.RenameColumn(
                name: "ÍnstalacionExpress",
                table: "DepuracionExportSiebel",
                newName: "VelocidadPrepago");

            migrationBuilder.RenameColumn(
                name: "Unnamed82",
                table: "DepuracionExportSiebel",
                newName: "UltimaModificion");

            migrationBuilder.RenameColumn(
                name: "Unnamed55",
                table: "DepuracionExportSiebel",
                newName: "TransferidoLibroTrabajoTransacciones");

            migrationBuilder.RenameColumn(
                name: "Unnamed53",
                table: "DepuracionExportSiebel",
                newName: "TipoCuentaPrepago");

            migrationBuilder.RenameColumn(
                name: "Unnamed27",
                table: "DepuracionExportSiebel",
                newName: "Sucursal");

            migrationBuilder.RenameColumn(
                name: "UltimaModificacion",
                table: "DepuracionExportSiebel",
                newName: "SubMotivoOrden");

            migrationBuilder.RenameColumn(
                name: "TransferidoTrabajoTransacciones",
                table: "DepuracionExportSiebel",
                newName: "RPT");

            migrationBuilder.RenameColumn(
                name: "Tipo1",
                table: "DepuracionExportSiebel",
                newName: "NumeroVTS");

            migrationBuilder.RenameColumn(
                name: "Telefonos",
                table: "DepuracionExportSiebel",
                newName: "NumeroCuenta");

            migrationBuilder.RenameColumn(
                name: "TelefonoPrincipal",
                table: "DepuracionExportSiebel",
                newName: "InstalacionExpress");

            migrationBuilder.RenameColumn(
                name: "PorcientoDescuento",
                table: "DepuracionExportSiebel",
                newName: "FechaOriginal");

            migrationBuilder.RenameColumn(
                name: "Organizacion",
                table: "DepuracionExportSiebel",
                newName: "EstatusActividades");

            migrationBuilder.RenameColumn(
                name: "OrdenSOA",
                table: "DepuracionExportSiebel",
                newName: "CuentaPrepago");
        }
    }
}
