using System.ComponentModel.DataAnnotations;

namespace AvanadeEstacionamento.Domain.DTO.Veiculo
{
    public class RequestVeiculoDTO
    {

        [Required(ErrorMessage = "É preciso informar a placa do veículo.")]
        [RegularExpression(@"^[A-Za-z]{3}-\d{4}$", ErrorMessage = "Placa inválida, formato deve ser ABC-1234.")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "É preciso informar um id de estacionamento.")]
        public Guid EstacionamentoId { get; set; }

    }
}
