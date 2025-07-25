﻿// <auto-generated />
using System;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fgc.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250722222439_Conta")]
    partial class Conta
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fgc.Domain.Biblioteca.Entidades.Genero", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id")
                        .HasName("PK_Genero");

                    b.ToTable("Generos", (string)null);
                });

            modelBuilder.Entity("Fgc.Domain.Biblioteca.Entidades.Jogo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao");

                    b.Property<DateTime>("DataLancamento")
                        .HasColumnType("datetime")
                        .HasColumnName("DataLancamento");

                    b.Property<string>("Desenvolvedora")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Desenvolvedora");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Preco");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Titulo");

                    b.HasKey("Id")
                        .HasName("PK_Jogo");

                    b.ToTable("Jogos", (string)null);
                });

            modelBuilder.Entity("Fgc.Domain.Usuario.Entidades.Conta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_Conta");

                    b.ToTable("Contas", (string)null);
                });

            modelBuilder.Entity("GeneroJogo", b =>
                {
                    b.Property<Guid>("GenerosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JogosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GenerosId", "JogosId");

                    b.HasIndex("JogosId");

                    b.ToTable("GeneroJogo");
                });

            modelBuilder.Entity("Fgc.Domain.Usuario.Entidades.Conta", b =>
                {
                    b.OwnsOne("Fgc.Domain.Usuario.ObjetosDeValor.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ContaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("VARCHAR")
                                .HasColumnName("Email");

                            b1.HasKey("ContaId");

                            b1.ToTable("Contas");

                            b1.WithOwner()
                                .HasForeignKey("ContaId");
                        });

                    b.OwnsOne("Fgc.Domain.Usuario.ObjetosDeValor.Senha", "Senha", b1 =>
                        {
                            b1.Property<Guid>("ContaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Senha");

                            b1.HasKey("ContaId");

                            b1.ToTable("Contas");

                            b1.WithOwner()
                                .HasForeignKey("ContaId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Senha")
                        .IsRequired();
                });

            modelBuilder.Entity("GeneroJogo", b =>
                {
                    b.HasOne("Fgc.Domain.Biblioteca.Entidades.Genero", null)
                        .WithMany()
                        .HasForeignKey("GenerosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fgc.Domain.Biblioteca.Entidades.Jogo", null)
                        .WithMany()
                        .HasForeignKey("JogosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
