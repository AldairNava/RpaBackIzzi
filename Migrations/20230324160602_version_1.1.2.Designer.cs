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
    [Migration("20230324160602_version_1.1.2")]
    partial class version112
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<DateTime?>("FechaCaptura")
                        .HasColumnType("datetime2");

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
#pragma warning restore 612, 618
        }
    }
}
