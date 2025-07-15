using Fgc.Domain.Biblioteca.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fgc.Infrastructure.Compartilhado.Data.Mappings
{
    public class GeneroMap : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Generos");

            builder.HasKey(x => x.Id)
                .HasName("PK_Genero");

            builder.Property(j => j.DataCriacao)
                .HasColumnName("DataCriacao")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(j => j.DataAlteracao)
                .HasColumnName("DataAlteracao")
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(x => x.Nome)
                .HasColumnType("nvarchar(100)")
                .IsRequired();
        }
    }
}
