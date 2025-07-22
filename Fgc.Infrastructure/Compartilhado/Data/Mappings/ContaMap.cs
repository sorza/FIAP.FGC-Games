using Fgc.Domain.Usuario.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fgc.Infrastructure.Compartilhado.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas");

            builder.HasKey(x => x.Id)
                .HasName("PK_Conta");

            builder.Property(x => x.Nome)
                .HasColumnType("VARCHAR(150)")
                .IsRequired(true);

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Address)
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(150)
                .IsRequired(true);
            });

            builder.OwnsOne(x => x.Senha, senha =>
            {
                senha.Property(s => s.Hash)
                .HasColumnName("Senha")
                .HasMaxLength(256)
                .IsRequired(true);
            });            
        }
    }
}
