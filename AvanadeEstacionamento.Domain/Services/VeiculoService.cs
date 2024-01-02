using AvanadeEstacionamento.Domain.Exceptions;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Interfaces.Service;
using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Services
{
    public class VeiculoService : IVeiculoService
    {

        #region Dependency Injection

        private readonly IVeiculoRepository _veiculoRepository;

        private readonly IEstacionamentoRepository _estacionamentoRepository;

        #endregion

        #region Constructor

        public VeiculoService(IVeiculoRepository veiculoRepository, IEstacionamentoRepository estacionamentoRepository)
        {
            _veiculoRepository = veiculoRepository;
            _estacionamentoRepository = estacionamentoRepository;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<VeiculoModel>> GetAll()
        {
            try
            {
                var result = await _veiculoRepository.GetAll();

                if (result == null || result.Count() == 0)
                {
                    throw new NotFoundException("Nenhum veículo cadastrado.");
                }
                return result;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }


        public async Task<IEnumerable<VeiculoModel>> GetByEstacionamentoId(Guid id)
        {
            try
            {
                var result = await _veiculoRepository.GetByEstacionamentoId(id);

                if (result == null || result.Count() == 0)
                {
                    throw new NotFoundException("Nenhum veículo vinculado a este estacionamento foi encontrado.");
                }
                return result;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<VeiculoModel> GetById(Guid id)
        {
            try
            {
                var result = await _veiculoRepository.GetById(id);

                if (result == null)
                {
                    throw new NotFoundException("Veículo não encontrado.");
                }
                return result;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<decimal> GetDebt(Guid id)
        {
            try
            {
                var veiculoModel = await _veiculoRepository.GetById(id);

                if (veiculoModel == null)
                {
                    throw new NotFoundException("Veículo não encontrado");
                }
                else
                {
                    var debt = await CalculateDebt(veiculoModel);
                    return debt;
                }
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<VeiculoModel> Create(VeiculoModel veiculo)
        {
            try
            {
                var isAlredyExistsVeiculo = await GetByPlaca(veiculo.Placa);

                if (!isAlredyExistsVeiculo)
                {
                    await _veiculoRepository.Create(veiculo);

                    return veiculo;
                }
                else
                {
                    throw new ResourceAlreadyExistsException("A placa informada já foi cadastrada, realize uma busca para verificar as informações completas do veículo.");
                }
            }
            catch (ResourceAlreadyExistsException ex)
            {
                throw new ResourceAlreadyExistsException(ex.Message);
            }

        }

        public async Task<bool> GetByPlaca(string placa)
        {
            var result = await _veiculoRepository.GetByPlaca(placa);

            if (result != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _veiculoRepository.Delete(id);

                if (result)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Falha ao deletar veículo");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> Update(VeiculoModel veiculo, Guid id)
        {
            try
            {
                if (veiculo.Id != id)
                {
                    throw new ArgumentException("Falha ao atualizar o veículo. O ID fornecido não corresponde ao ID do veículo repassado.");
                }
                else
                {
                    var result = await _veiculoRepository.Update(veiculo);

                    if (result)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<decimal> Checkout(Guid id)
        {
            try
            {
                var veiculoModel = await _veiculoRepository.GetById(id);

                if (veiculoModel == null)
                {
                    throw new NotFoundException("Veículo não encontrado");
                }
                else
                {
                    #region Desativando veiculo

                    veiculoModel.IsAtivo = false;

                    veiculoModel.DataCheckout = DateTime.Now;

                    await Update(veiculoModel, id);

                    #endregion

                    var debt = await GetDebt(id);

                    return debt;
                }
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private async Task<decimal> CalculateDebt(VeiculoModel veiculoModel)
        {
            var currentDate = DateTime.Now;

            var totalTimeSpent = (decimal)(currentDate - veiculoModel.DataCriacao).TotalHours;

            var estacionamento = await _estacionamentoRepository.GetById(veiculoModel.EstacionamentoId);

            return estacionamento.PrecoInicial + (estacionamento.PrecoHora * totalTimeSpent);
        }

        #endregion

    }
}
