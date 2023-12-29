using AvanadeEstacionamento.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvanadeEstacionamento.Data.Map
{
    public class EstacionamentoMap : IEntityTypeConfiguration<EstacionamentoModel>
    {
        public void Configure(EntityTypeBuilder<EstacionamentoModel> builder)
        {
            builder.HasKey(estacionamento => estacionamento.Id);

            builder.Property(estacionamento => estacionamento.PrecoInicial)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)");

            builder.Property(estacionamento => estacionamento.PrecoHora)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)");

            builder.HasMany(estacionamento => estacionamento.VeiculoL)
                   .WithOne(veiculo => veiculo.Estacionamento)
                   .HasForeignKey(veiculo => veiculo.EstacionamentoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(estacionamento => estacionamento.DataCriacao);

            builder.Property(estacionamento => estacionamento.IsAtivo);

            builder.ToTable("Estacionamento");
        }
    }
}
