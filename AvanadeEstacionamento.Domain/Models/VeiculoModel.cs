
namespace AvanadeEstacionamento.Domain.Models
{
    public class VeiculoModel : BaseModel
    {
        public string Placa { get; set; }


        public Guid EstacionamentoId { get; set; }
        public EstacionamentoModel Estacionamento { get; set; }

    }
}
