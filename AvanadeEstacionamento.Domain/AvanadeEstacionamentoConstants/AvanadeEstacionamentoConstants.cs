namespace AvanadeEstacionamento.API.EstacionamentoConstants
{
    public static class AvanadeEstacionamentoConstants
    {
        #region Constants for veiculo exceptions

        public const string VEICULO_NOT_FOUND_EXCEPTION = "Nenhum veículo cadastrado.";

        public const string VEICULO_BY_ESTACIONAMENTO_NOT_FOUND = "Nenhum veículo vinculado a este estacionamento foi encontrado.";

        public const string VEICULO_BY_PLACA_ALREADY_EXISTS = "A placa informada já foi cadastrada, realize uma busca para verificar as informações completas do veículo.";

        public const string VEICULO_DELETE_FAIL = "Falha ao deletar veículo.";

        public const string VEICULO_UPDATE_FAIL = "Falha ao atualizar o veículo. O ID fornecido não corresponde ao ID do veículo repassado.";

        #endregion


    }
}
