﻿// <auto-generated />
using System;
using AvanadeEstacionamento.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AvanadeEstacionamento.Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AvanadeEstacionamento.Domain.Models.EstacionamentoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PrecoHora")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("PrecoInicial")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Estacionamento", (string)null);
                });

            modelBuilder.Entity("AvanadeEstacionamento.Domain.Models.VeiculoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EstacionamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstacionamentoId");

                    b.ToTable("Veiculo", (string)null);
                });

            modelBuilder.Entity("AvanadeEstacionamento.Domain.Models.VeiculoModel", b =>
                {
                    b.HasOne("AvanadeEstacionamento.Domain.Models.EstacionamentoModel", "Estacionamento")
                        .WithMany("VeiculoL")
                        .HasForeignKey("EstacionamentoId")
                        .IsRequired();

                    b.Navigation("Estacionamento");
                });

            modelBuilder.Entity("AvanadeEstacionamento.Domain.Models.EstacionamentoModel", b =>
                {
                    b.Navigation("VeiculoL");
                });
#pragma warning restore 612, 618
        }
    }
}
