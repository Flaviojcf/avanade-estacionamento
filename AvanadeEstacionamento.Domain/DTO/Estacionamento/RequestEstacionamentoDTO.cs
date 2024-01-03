using System.ComponentModel.DataAnnotations;

namespace AvanadeEstacionamento.Domain.DTO.Estacionamento
{
    public class RequestEstacionamentoDTO
    {
        [Required(ErrorMessage = "O preço inicial é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço inicial deve ser um valor maior que zero.")]
        public decimal PrecoInicial { get; set; }

        [Required(ErrorMessage = "O preço por hora é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço por hora deve ser um valor maior que zero.")]
        public decimal PrecoHora { get; set; }
    }
}
