using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AvanadeEstacionamento.Domain.Models
{
    public class EstacionamentoModel : BaseModel
    {
        [Required(ErrorMessage = "O preço inicial é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço inicial deve ser um valor maior que zero.")]
        public decimal PrecoInicial { get; set; }

        [Required(ErrorMessage = "O preço por hora é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço por hora deve ser um valor maior que zero.")]
        public decimal PrecoHora { get; set; }

        [Required(ErrorMessage = "O nome do estacionamento é obrigatório.")]
        public string Nome { get; set; }

        [JsonIgnore]
        public List<VeiculoModel>? VeiculoL { get; set; }
    }


}
