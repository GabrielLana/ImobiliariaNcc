using ImobiliariaNcc.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImobiliariaNcc.Infrastructure.Persistence.Configurations;

public class ReservasConfiguration : IEntityTypeConfiguration<ReservaModel>
{
    public void Configure(EntityTypeBuilder<ReservaModel> builder)
    {
        builder.ToTable("reservas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataCriacao)
            .HasColumnName("data_criacao").IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        builder.Property(x => x.DataAlteracao)
            .HasColumnName("data_alteracao").IsRequired(false);
        builder.Property(x => x.Ativo)
            .HasColumnName("ativo").IsRequired(true).HasDefaultValue(true);
        builder.Property(x => x.IdCliente)
            .HasColumnName("ID_CLIENTE").IsRequired(true);
        builder.Property(x => x.IdApartamento)
            .HasColumnName("ID_APARTAMENTO").IsRequired(true);
    }
}
