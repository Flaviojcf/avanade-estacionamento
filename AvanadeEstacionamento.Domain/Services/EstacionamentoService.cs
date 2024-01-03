using AutoMapper;
using AvanadeEstacionamento.API.EstacionamentoConstants;
using AvanadeEstacionamento.Domain.DTO.Estacionamento;
using AvanadeEstacionamento.Domain.Exceptions;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Interfaces.Service;
using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Services
{
    public class EstacionamentoService : IEstacionamentoService
    {

        #region Dependency Injection

        private readonly IEstacionamentoRepository _estacionamentoRepository;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public EstacionamentoService(IEstacionamentoRepository estacionamentoRepository, IMapper mapper)
        {
            _estacionamentoRepository = estacionamentoRepository;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<EstacionamentoModel>> GetAll()
        {
            try
            {
                var result = await _estacionamentoRepository.GetAll();

                if (result == null || result.Count() == 0)
                {
                    throw new NotFoundException(AvanadeEstacionamentoConstants.ANY_ESTACIONAMENTO_HAS_BEEN_REGISTERED_EXCEPTION);
                }
                return result;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<EstacionamentoModel> GetById(Guid id)
        {
            try
            {
                var result = await _estacionamentoRepository.GetById(id);

                if (result == null)
                {
                    throw new NotFoundException(AvanadeEstacionamentoConstants.ESTACIONAMENTO_NOT_FOUND_EXCEPTION);
                }
                return result;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<EstacionamentoModel> Create(RequestEstacionamentoDTO estacionamentoDTO)
        {
            try
            {
                var estacionamentoModel = _mapper.Map<EstacionamentoModel>(estacionamentoDTO);
                var result = await _estacionamentoRepository.Create(estacionamentoModel);

                if (result)
                {
                    return estacionamentoModel;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _estacionamentoRepository.Delete(id);

                if (result)
                {
                    return true;
                }
                else
                {
                    throw new Exception(AvanadeEstacionamentoConstants.ESTACIONAMENTO_DELETE_FAIL_EXCEPTION);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(EstacionamentoModel estacionamento, Guid id)
        {
            try
            {
                if (estacionamento.Id != id)
                {
                    throw new ArgumentException(AvanadeEstacionamentoConstants.ESTACIONAMENTO_UPDATE_FAIL_EXCEPTION);
                }
                else
                {
                    estacionamento.DataAlteracao = DateTime.Now;
                    var result = await _estacionamentoRepository.Update(estacionamento);

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
