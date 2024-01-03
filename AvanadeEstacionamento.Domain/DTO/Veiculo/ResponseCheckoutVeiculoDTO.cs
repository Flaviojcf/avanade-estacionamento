
namespace AvanadeEstacionamento.Domain.DTO.Veiculo
{
    public class ResponseCheckoutVeiculoDTO
    {
        public DateTime DataCriacao { get; set; }

        public DateTime? DataCheckout { get; set; }

        public decimal TotalDebt { get; set; }
    }
}
