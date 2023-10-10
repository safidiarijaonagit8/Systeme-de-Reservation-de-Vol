﻿// <auto-generated />
using GestionReservation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestionReservation.Migrations
{
    [DbContext(typeof(GestionReservationContext))]
    [Migration("20230918143914_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestionReservation.Models.Avion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Modele")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbrSiegeAffaire")
                        .HasColumnType("int");

                    b.Property<int>("NbrSiegeEconomique")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Avion");
                });
#pragma warning restore 612, 618
        }
    }
}