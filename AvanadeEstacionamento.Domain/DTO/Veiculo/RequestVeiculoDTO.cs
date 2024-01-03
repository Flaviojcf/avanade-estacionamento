namespace AvanadeEstacionamento.Domain.DTO.Veiculo
{
    public class RequestVeiculoDTO
    {
        public Guid? Id { get; set; }

        public string Placa { get; set; }

        public Guid EstacionamentoId { get; set; }

    }
}
