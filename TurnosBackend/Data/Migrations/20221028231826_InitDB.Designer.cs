﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(TurnosAsesoftwareContext))]
    [Migration("20221028231826_InitDB")]
    partial class InitDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Modelos.Cliente", b =>
                {
                    b.Property<int>("id_cliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ape_cliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id_direccion")
                        .HasColumnType("int");

                    b.Property<string>("nid_cliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nom_cliente")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id_cliente");

                    b.HasIndex("id_direccion");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Modelos.Direccion", b =>
                {
                    b.Property<int>("id_direccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("barrio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("calle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pais")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id_direccion");

                    b.ToTable("Direccion");
                });

            modelBuilder.Entity("Modelos.Cliente", b =>
                {
                    b.HasOne("Modelos.Direccion", "Direccion")
                        .WithMany()
                        .HasForeignKey("id_direccion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direccion");
                });
#pragma warning restore 612, 618
        }
    }
}
