using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fgc.Infrastructure.Compartilhado.Data.Mappings
{
    public class BibliotecaMap : IEntityTypeConfiguration<Domain.Biblioteca.Entidades.Biblioteca>
    {
        public void Configure(EntityTypeBuilder<Domain.Biblioteca.Entidades.Biblioteca> builder)
        {
            builder.ToTable("Biblioteca");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(b => b.ContaId)
                .HasColumnName("ContaId")
                .IsRequired();

            builder.HasOne(b => b.Conta)
                .WithMany()
                .HasForeignKey(b => b.ContaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Jogos)
                .WithOne(j => j.Biblioteca)
                .HasForeignKey(j => j.BibliotecaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
