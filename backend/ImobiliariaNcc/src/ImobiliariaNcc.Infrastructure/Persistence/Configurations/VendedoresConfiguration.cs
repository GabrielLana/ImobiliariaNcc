using ImobiliariaNcc.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImobiliariaNcc.Infrastructure.Persistence.Configurations;

public class VendedoresConfiguration : IEntityTypeConfiguration<VendedorModel>
{
    public void Configure(EntityTypeBuilder<VendedorModel> builder)
    {
        builder.ToTable("vendedores");

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
        builder.Property(x => x.Senha)
            .HasColumnName("senha").IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.DataNascimento)
            .HasColumnName("data_nascimento").IsRequired(true);
        builder.Property(x => x.Email)
            .HasColumnName("email").IsRequired(true).HasMaxLength(200);
        builder.Property(x => x.Celular)
            .HasColumnName("celular").IsRequired(true).HasMaxLength(11);
        builder.Property(x => x.Cep)
            .HasColumnName("cep").IsRequired(true).HasMaxLength(8);
        builder.Property(x => x.Logradouro)
            .HasColumnName("logradouro").IsRequired(true).HasMaxLength(150);
        builder.Property(x => x.Bairro)
            .HasColumnName("bairro").IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.Numero)
            .HasColumnName("numero").IsRequired(true).HasMaxLength(5);
        builder.Property(x => x.Complemento)
            .HasColumnName("complemento").IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.Setor)
            .HasColumnName("setor").IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.NumeroRegistro)
            .HasColumnName("numero_registro").IsRequired(true);
    }
}