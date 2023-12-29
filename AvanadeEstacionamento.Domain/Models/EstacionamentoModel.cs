namespace AvanadeEstacionamento.Domain.Models
{
    public class EstacionamentoModel
    {
        decimal PrecoInicial { get; set; }

        decimal PrecoHora { get; set; }

        List<VeiculoModel> VeiculoL { get; set; }
    }
}
