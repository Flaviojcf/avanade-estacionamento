
namespace AvanadeEstacionamento.Domain.Models
{
    public class VeiculoModel : BaseModel
    {
        public string Placa { get; set; }


        public int EstacionamentoId { get; set; }
        public EstacionamentoModel Estacionamento { get; set; }

    }
}
