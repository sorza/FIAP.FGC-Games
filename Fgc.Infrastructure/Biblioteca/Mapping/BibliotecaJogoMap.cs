using Fgc.Domain.Biblioteca.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fgc.Infrastructure.Biblioteca.Mapping
{
    public class BibliotecaJogoMap : IEntityTypeConfiguration<BibliotecaJogo>
    {
        public void Configure(EntityTypeBuilder<BibliotecaJogo> builder)
        {
            builder.ToTable("BibliotecaJogo");

            builder.HasKey(bj => bj.Id);

            builder.Property(bj => bj.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(bj => bj.BibliotecaId)
                .HasColumnName("BibliotecaId")
                .IsRequired();

            builder.HasOne(bj => bj.Biblioteca)
                .WithMany(b => b.Jogos)
                .HasForeignKey(bj => bj.BibliotecaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(bj => bj.JogoId)
                .HasColumnName("JogoId")
                .IsRequired();

            builder.HasOne(bj => bj.Jogo)
                .WithMany()
                .HasForeignKey(bj => bj.JogoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(bj => bj.DataAquisicao)
                .HasColumnName("DataAquisicao")
                .IsRequired();

            builder.Property(bj => bj.ValorPago)
                .HasColumnName("ValorPago")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
