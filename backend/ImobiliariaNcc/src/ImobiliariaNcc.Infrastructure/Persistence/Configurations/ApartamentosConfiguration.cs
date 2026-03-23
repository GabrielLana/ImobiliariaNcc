using ImobiliariaNcc.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImobiliariaNcc.Infrastructure.Persistence.Configurations;

public class ApartamentosConfiguration : IEntityTypeConfiguration<ApartamentoModel>
{
    public void Configure(EntityTypeBuilder<ApartamentoModel> builder)
    {
        builder.ToTable("apartamentos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataCriacao)
            .HasColumnName("data_criacao").IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        builder.Property(x => x.DataAlteracao)
            .HasColumnName("data_alteracao").IsRequired(false);
        builder.Property(x => x.Ocupado)
            .HasColumnName("ocupado").IsRequired(true).HasDefaultValue(false);
        builder.Property(x => x.Metragem)
            .HasColumnName("metragem").IsRequired(true);
        builder.Property(x => x.Quartos)
            .HasColumnName("quartos").IsRequired(true);
        builder.Property(x => x.Banheiros)
            .HasColumnName("banheiros").IsRequired(true);
        builder.Property(x => x.Vagas)
            .HasColumnName("vagas").IsRequired(true);
        builder.Property(x => x.DetalhesApartamento)
            .HasColumnName("detalhes_apartamento").IsRequired(true);
        builder.Property(x => x.DetalhesCondominio)
            .HasColumnName("detalhes_condominio").IsRequired(true);
        builder.Property(x => x.Andar)
            .HasColumnName("andar").IsRequired(true);
        builder.Property(x => x.Bloco)
            .HasColumnName("bloco").IsRequired(true);
        builder.Property(x => x.ValorVenda)
            .HasColumnName("valor_venda").IsRequired(true);
        builder.Property(x => x.ValorCondominio)
            .HasColumnName("valor_condominio").IsRequired(true);
        builder.Property(x => x.ValorIptu)
            .HasColumnName("valor_iptu").IsRequired(true);
        builder.Property(x => x.Cep)
            .HasColumnName("cep").IsRequired(true).HasMaxLength(8);
        builder.Property(x => x.Logradouro)
            .HasColumnName("logradouro").IsRequired(true).HasMaxLength(150);
        builder.Property(x => x.Bairro)
            .HasColumnName("bairro").IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.Numero)
            .HasColumnName("numero").IsRequired(true).HasMaxLength(5);
        builder.Property(x => x.Estado)
            .HasColumnName("estado").IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Cidade)
            .HasColumnName("cidade").IsRequired(true).HasMaxLength(200);
        builder.Property(x => x.Complemento)
            .HasColumnName("complemento").IsRequired(false).HasMaxLength(100);
    }
}