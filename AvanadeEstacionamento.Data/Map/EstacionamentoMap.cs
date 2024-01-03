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

            builder.Property(estacionamento => estacionamento.Nome)
                   .IsRequired()
                   .HasColumnName("estacionamento_nome");

            builder.Property(estacionamento => estacionamento.PrecoInicial)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)")
                   .HasColumnName("preco_inicial");

            builder.Property(estacionamento => estacionamento.PrecoHora)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)")
                   .HasColumnName("preco_hora");

            builder.HasMany(estacionamento => estacionamento.VeiculoL)
                   .WithOne(veiculo => veiculo.Estacionamento)
                   .HasForeignKey(veiculo => veiculo.EstacionamentoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(estacionamento => estacionamento.DataCriacao)
                   .HasColumnName("dth_cadastro");

            builder.Property(estacionamento => estacionamento.IsAtivo)
                   .HasColumnType("bit")
                   .IsRequired()
                   .HasColumnName("ind_ativo");

            builder.Property(estacionamento => estacionamento.DataAlteracao)
                   .IsRequired(false)
                   .HasColumnName("dth_alteracao");

            builder.ToTable("tb_estacionamento");
        }
    }
}
