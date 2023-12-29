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

            builder.ToTable("Veiculo");
        }
    }
}
