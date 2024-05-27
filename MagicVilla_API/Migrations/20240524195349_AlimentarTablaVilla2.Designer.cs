﻿// <auto-generated />
using System;
using MagicVilla_API.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240524195349_AlimentarTablaVilla2")]
    partial class AlimentarTablaVilla2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_API.Modelos.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "Nadar",
                            Detalle = "Lago Hermoso",
                            FechaActualizacion = new DateTime(2024, 5, 24, 13, 53, 49, 209, DateTimeKind.Local).AddTicks(2808),
                            FechaCreacion = new DateTime(2024, 5, 24, 13, 53, 49, 209, DateTimeKind.Local).AddTicks(2794),
                            ImageUrl = "",
                            MetrosCuadrados = 5000,
                            Nombre = "Villa Zirahuen",
                            Ocupantes = 1000,
                            Tarifa = 500.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "Nadar contracorriente",
                            Detalle = "Rio Hermoso",
                            FechaActualizacion = new DateTime(2024, 5, 24, 13, 53, 49, 209, DateTimeKind.Local).AddTicks(2812),
                            FechaCreacion = new DateTime(2024, 5, 24, 13, 53, 49, 209, DateTimeKind.Local).AddTicks(2812),
                            ImageUrl = "",
                            MetrosCuadrados = 1000,
                            Nombre = "Villa Tarejero",
                            Ocupantes = 300,
                            Tarifa = 200.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
