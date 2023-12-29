using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Interfaces.Service;
using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Services
{
    public class EstacionamentoService : IEstacionamentoService
    {

        #region Dependency Injection

        private readonly IEstacionamentoRepository _estacionamentoRepository;

        #endregion

        #region Constructor

        public EstacionamentoService(IEstacionamentoRepository estacionamentoRepository)
        {
            _estacionamentoRepository = estacionamentoRepository;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<EstacionamentoModel>> GetAll()
        {
            return await _estacionamentoRepository.GetAll();
        }

        public async Task<EstacionamentoModel> GetById(Guid id)
        {
            return await _estacionamentoRepository.GetById(id);
        }

        public async Task<EstacionamentoModel> Create(EstacionamentoModel estacionamento)
        {
            await _estacionamentoRepository.Create(estacionamento);
            return estacionamento;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _estacionamentoRepository.Delete(id);
        }

        public async Task<bool> Update(EstacionamentoModel estacionamento, Guid id)
        {
            if (estacionamento.Id != id) return false;

            var result = await _estacionamentoRepository.Update(estacionamento);

            if (result)
            {
                return true;
            }
            else
            {
                throw new Exception("Falha ao editar o estacionamento");
            }
        }

        #endregion
    }
}
