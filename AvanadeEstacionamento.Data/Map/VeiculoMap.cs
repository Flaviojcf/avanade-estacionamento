using AvanadeEstacionamento.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvanadeEstacionamento.Data.Map
{
    public class VeiculoMap : IEntityTypeConfiguration<VeiculoModel>
    {
        public void Configure(EntityTypeBuilder<VeiculoModel> builder)
        {
            builder.HasKey(veiculo => veiculo.Id);

            builder.HasOne(veiculo => veiculo.Estacionamento)
                   .WithMany(estacionamento => estacionamento.VeiculoL)
                   .HasForeignKey(veiculo => veiculo.EstacionamentoId);

            builder.Property(veiculo => veiculo.DataCriacao)
                   .HasColumnName("dth_cadastro");

            builder.Property(veiculo => veiculo.DataCheckout)
                   .IsRequired(false)
                   .HasColumnName("dth_checkout");

            builder.Property(veiculo => veiculo.IsAtivo)
                   .HasColumnType("bit")
                   .IsRequired()
                   .HasColumnName("ind_ativo");

            builder.Property(estacionamento => estacionamento.DataAlteracao)
                   .HasColumnName("dth_alteracao");

            builder.ToTable("tb_veiculo");
        }
    }
}
