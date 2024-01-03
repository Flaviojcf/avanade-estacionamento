﻿namespace AvanadeEstacionamento.API.EstacionamentoConstants
{
    public static class AvanadeEstacionamentoConstants
    {
        #region Constants for veiculo exceptions

        public const string VEICULO_NOT_FOUND_EXCEPTION = "Veículo não encontrado.";

        public const string ANY_VEICULO_HAS_BEEN_REGISTERED_EXCEPTION = "Nenhum veículo cadastrado.";

        public const string VEICULO_BY_ESTACIONAMENTO_NOT_FOUND_EXCEPTION = "Nenhum veículo vinculado a este estacionamento foi encontrado.";

        public const string VEICULO_BY_PLACA_ALREADY_EXISTS_EXCEPTION = "A placa informada já foi cadastrada, realize uma busca para verificar as informações completas do veículo.";

        public const string VEICULO_DELETE_FAIL_EXCEPTION = "Falha ao deletar veículo.";

        public const string VEICULO_UPDATE_FAIL_EXCEPTION = "Falha ao atualizar o veículo. O ID fornecido não corresponde ao ID do veículo repassado.";

        #endregion


        #region Constants for estacionamento exceptions

        public const string ESTACIONAMENTO_NOT_FOUND_EXCEPTION = "Estacionamento não encontrado.";

        public const string ANY_ESTACIONAMENTO_HAS_BEEN_REGISTERED_EXCEPTION = "Nenhum estacionamento cadastrado.";

        public const string ESTACIONAMENTO_DELETE_FAIL_EXCEPTION = "Falha ao deletar estacionamento.";

        public const string ESTACIONAMENTO_UPDATE_FAIL_EXCEPTION = "Falha ao atualizar o estacionamento. O ID fornecido não corresponde ao ID do estacionamento repassado.";

        #endregion

        public const string GLOBAL_MESSAGE_FOR_INTERNAL_ERROR_EXCEPTION = "Ocorreu um erro interno. Entre em contato com nossa equipe para mais informações. Visite: https://github.com/Flaviojcf ";

    }
}
