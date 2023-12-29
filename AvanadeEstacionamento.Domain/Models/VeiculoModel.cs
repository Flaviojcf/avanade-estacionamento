
namespace AvanadeEstacionamento.Domain.Models
{
    public class VeiculoModel : BaseModel
    {
        public string Placa { get; set; }

        public EstacionamentoModel Estacionamento { get; set; }

    }
}
