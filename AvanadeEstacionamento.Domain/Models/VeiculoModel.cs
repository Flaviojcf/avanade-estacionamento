
using System.Text.Json.Serialization;

namespace AvanadeEstacionamento.Domain.Models
{
    public class VeiculoModel : BaseModel
    {
        public string Placa { get; set; }


        public Guid EstacionamentoId { get; set; }

        [JsonIgnore]
        public EstacionamentoModel? Estacionamento { get; set; }

    }
}
