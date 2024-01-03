﻿// <auto-generated />
using System;
using AvanadeEstacionamento.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AvanadeEstacionamento.Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240103015428_editColumnName")]
    partial class editColumnName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("dth_alteracao");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2")
                        .HasColumnName("dth_cadastro");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("bit")
                        .HasColumnName("ind_ativo");

                    b.Property<decimal>("PrecoHora")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("preco_hora");

                    b.Property<decimal>("PrecoInicial")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("preco_inicial");

                    b.HasKey("Id");

                    b.ToTable("tb_estacionamento", (string)null);
                });

            modelBuilder.Entity("AvanadeEstacionamento.Domain.Models.VeiculoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("dth_alteracao");

                    b.Property<DateTime?>("DataCheckout")
                        .HasColumnType("datetime2")
                        .HasColumnName("dth_checkout");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2")
                        .HasColumnName("dth_cadastro");

                    b.Property<Guid>("EstacionamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("bit")
                        .HasColumnName("ind_ativo");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstacionamentoId");

                    b.ToTable("tb_veiculo", (string)null);
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