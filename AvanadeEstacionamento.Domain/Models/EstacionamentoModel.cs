namespace AvanadeEstacionamento.Domain.Models
{
    public class EstacionamentoModel : BaseModel
    {
       public decimal PrecoInicial { get; set; }

       public decimal PrecoHora { get; set; }

       public List<VeiculoModel> VeiculoL { get; set; }
    }
}
