using Fgc.Domain.Biblioteca.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fgc.Infrastructure.Compartilhado.Data.Mappings
{
    public class JogoMap : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.ToTable("Jogos");            

            builder.HasKey(x => x.Id)
                .HasName("PK_Jogo");

            builder.Property(j => j.DataCriacao)
                .HasColumnName("DataCriacao")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(j => j.DataAlteracao)
                .HasColumnName("DataAlteracao")
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(x => x.Titulo)
                .HasColumnName("Titulo")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.Property(x => x.Preco)
                .HasColumnName("Preco")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x=> x.AnoLancamento)
                .HasColumnName("AnoLancamento")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Desenvolvedora)
                .HasColumnName("Desenvolvedora")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder
              .HasMany(j => j.Generos)
              .WithMany(g => g.Jogos);

        }
    }
}
