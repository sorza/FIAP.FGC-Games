﻿// <auto-generated />
using System;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fgc.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(100)");

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
