using AutoMapper;
using AvanadeEstacionamento.API.EstacionamentoConstants;
using AvanadeEstacionamento.Domain.DTO.Veiculo;
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

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public VeiculoService(IVeiculoRepository veiculoRepository, IEstacionamentoRepository estacionamentoRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _estacionamentoRepository = estacionamentoRepository;
            _mapper = mapper;
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

        public async Task<VeiculoModel> Create(RequestVeiculoDTO veiculoDTO)
        {
            try
            {
                var isAlreadyExistsVeiculo = await GetByPlaca(veiculoDTO.Placa);

                if (isAlreadyExistsVeiculo)
                {
                    throw new ResourceAlreadyExistsException(AvanadeEstacionamentoConstants.VEICULO_BY_PLACA_ALREADY_EXISTS_EXCEPTION);
                }

                var estacionamentoExists = await _estacionamentoRepository.GetById(veiculoDTO.EstacionamentoId);

                if (estacionamentoExists == null)
                {
                    throw new NotFoundException(AvanadeEstacionamentoConstants.ESTACIONAMENTO_NOT_FOUND_EXCEPTION);
                }

                var veiculoModel = _mapper.Map<VeiculoModel>(veiculoDTO);
                await _veiculoRepository.Create(veiculoModel);

                return veiculoModel;
            }
            catch (ResourceAlreadyExistsException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
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

        public async Task<bool> Update(RequestUpdateVeiculoDTO veiculoDTO, Guid id)
        {
            try
            {
                if (veiculoDTO.Id != id)
                {
                    throw new ArgumentException(AvanadeEstacionamentoConstants.VEICULO_UPDATE_FAIL_EXCEPTION);
                }
                else
                {

                    var isAlreadyExistsVeiculo = await GetByPlaca(veiculoDTO.Placa);

                    if (isAlreadyExistsVeiculo)
                    {
                        throw new ResourceAlreadyExistsException(AvanadeEstacionamentoConstants.VEICULO_BY_PLACA_ALREADY_EXISTS_EXCEPTION);
                    }

                    var veiculoInfo = await GetById(id);
                    var veiculoModel = _mapper.Map<VeiculoModel>(veiculoDTO);

                    veiculoModel.DataAlteracao = DateTime.Now;
                    veiculoModel.EstacionamentoId = veiculoInfo.EstacionamentoId;
                    var result = await _veiculoRepository.Update(veiculoModel);

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
            catch(ResourceAlreadyExistsException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<ResponseCheckoutVeiculoDTO> Checkout(Guid id)
        {
            try
            {
                var veiculoModel = await GetById(id);

                var debt = await GetDebt(id);

                #region Desativando veiculo

                veiculoModel.IsAtivo = false;
                veiculoModel.DataAlteracao = DateTime.Now;
                veiculoModel.DataCheckout = DateTime.Now;

                await UpdateModel(veiculoModel, id);

                #endregion

                return new ResponseCheckoutVeiculoDTO { DataCheckout = veiculoModel.DataCheckout, DataCriacao = veiculoModel.DataCriacao, TotalDebt = debt };
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

        private async Task<bool> UpdateModel(VeiculoModel veiculo, Guid id)
        {
            try
            {
                if (veiculo.Id != id)
                {
                    throw new ArgumentException(AvanadeEstacionamentoConstants.VEICULO_UPDATE_FAIL_EXCEPTION);
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

        #endregion

    }
}
