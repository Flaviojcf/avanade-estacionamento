using AvanadeEstacionamento.API.EstacionamentoConstants;
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
                    throw new NotFoundException(AvanadeEstacionamentoConstants.ANY_VEICULO_HAS_BEEN_REGISTERED_EXCEPTION);
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
                    throw new NotFoundException(AvanadeEstacionamentoConstants.VEICULO_BY_ESTACIONAMENTO_NOT_FOUND_EXCEPTION);
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
                    throw new NotFoundException(AvanadeEstacionamentoConstants.VEICULO_NOT_FOUND_EXCEPTION);
                }
                else if (!result.IsAtivo)
                {
                    throw new AlreadyCheckoutVeiculo(AvanadeEstacionamentoConstants.VEHICLE_HAS_ALREADY_BEEN_CHECKED_OUT_EXCEPTION);
                }
                return result;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (AlreadyCheckoutVeiculo ex)
            {
                throw new AlreadyCheckoutVeiculo(ex.Message);
            }
        }

        public async Task<decimal> GetDebt(Guid id)
        {
            try
            {
                var veiculoModel = await _veiculoRepository.GetById(id);

                if (veiculoModel == null)
                {
                    throw new NotFoundException(AvanadeEstacionamentoConstants.VEICULO_NOT_FOUND_EXCEPTION);
                }
                else if (!veiculoModel.IsAtivo)
                {
                    throw new AlreadyCheckoutVeiculo(AvanadeEstacionamentoConstants.VEHICLE_HAS_ALREADY_BEEN_CHECKED_OUT_EXCEPTION);
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
            catch (AlreadyCheckoutVeiculo ex)
            {
                throw new AlreadyCheckoutVeiculo(ex.Message);
            }
        }

        public async Task<VeiculoModel> Create(VeiculoModel veiculo)
        {
            try
            {
                var isAlredyExistsVeiculo = await GetByPlaca(veiculo.Placa);

                if (!isAlredyExistsVeiculo)
                {
                    var estacionamentoExists = await _estacionamentoRepository.GetById(veiculo.EstacionamentoId);

                    if (estacionamentoExists != null)
                    {
                        await _veiculoRepository.Create(veiculo);
                    }
                    else
                    {
                        throw new NotFoundException(AvanadeEstacionamentoConstants.ESTACIONAMENTO_NOT_FOUND_EXCEPTION);
                    }

                    return veiculo;
                }
                else
                {
                    throw new ResourceAlreadyExistsException(AvanadeEstacionamentoConstants.VEICULO_BY_PLACA_ALREADY_EXISTS_EXCEPTION);
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
                    throw new Exception(AvanadeEstacionamentoConstants.VEICULO_DELETE_FAIL_EXCEPTION);
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
                    throw new ArgumentException(AvanadeEstacionamentoConstants.VEICULO_UPDATE_FAIL_EXCEPTION);
                }
                else
                {
                    veiculo.DataAlteracao = DateTime.Now;
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
                    throw new NotFoundException(AvanadeEstacionamentoConstants.VEICULO_NOT_FOUND_EXCEPTION);
                }
                else if (!veiculoModel.IsAtivo)
                {
                    throw new AlreadyCheckoutVeiculo(AvanadeEstacionamentoConstants.VEHICLE_HAS_ALREADY_BEEN_CHECKED_OUT_EXCEPTION);
                }
                else
                {
                    #region Desativando veiculo

                    veiculoModel.IsAtivo = false;
                    veiculoModel.DataAlteracao = DateTime.Now;
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
            catch (AlreadyCheckoutVeiculo ex)
            {
                throw new AlreadyCheckoutVeiculo(ex.Message);
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
