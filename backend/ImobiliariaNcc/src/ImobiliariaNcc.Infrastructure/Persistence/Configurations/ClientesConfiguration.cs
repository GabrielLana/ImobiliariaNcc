using ImobiliariaNcc.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImobiliariaNcc.Infrastructure.Persistence.Configurations;

public class ClientesConfiguration : IEntityTypeConfiguration<ClienteModel>
{
    public void Configure(EntityTypeBuilder<ClienteModel> builder)
    {
        builder.ToTable("clientes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataCriacao)
            .HasColumnName("data_criacao").IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        builder.Property(x => x.DataAlteracao)
            .HasColumnName("data_alteracao").IsRequired(false);
        builder.Property(x => x.Ativo)
            .HasColumnName("ativo").IsRequired(true).HasDefaultValue(true);
        builder.Property(x => x.Nome)
            .HasColumnName("nome").IsRequired(true).HasMaxLength(200);
        builder.Property(x => x.Cpf)
            .HasColumnName("cpf").IsRequired(true).HasMaxLength(11);
        builder.Property(x => x.DataNascimento)
            .HasColumnName("data_nascimento").IsRequired(true);
        builder.Property(x => x.Email)
            .HasColumnName("email").IsRequired(true).HasMaxLength(200);
        builder.Property(x => x.Celular)
            .HasColumnName("celular").IsRequired(true).HasMaxLength(11);
        builder.Property(x => x.EstadoCivil)
            .HasColumnName("estado_civil").IsRequired(true).HasMaxLength(20);
    }
}
