using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Interfaces.Service;
using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Services
{
    public class VeiculoService : IVeiculoService
    {

        #region Dependency Injection

        private readonly IVeiculoRepository _veiculoRepository;

        #endregion

        #region Constructor

        public VeiculoService(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<VeiculoModel>> GetAll()
        {
            return await _veiculoRepository.GetAll();
        }

        public async Task<IEnumerable<VeiculoModel>> GetByEstacionamentoId(Guid id)
        {
            return await _veiculoRepository.GetByEstacionamentoId(id);
        }

        public async Task<VeiculoModel> GetById(Guid id)
        {
            return await _veiculoRepository.GetById(id);
        }

        public async Task<VeiculoModel> Create(VeiculoModel veiculo)
        {
            await _veiculoRepository.Create(veiculo);

            return veiculo;
        }

        public async Task<bool> Delete(Guid id)
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

        public async Task<bool> Update(VeiculoModel veiculo, Guid id)
        {
            if (veiculo.Id != id) return false;

            var result = await _veiculoRepository.Update(veiculo);

            if (result)
            {
                return true;
            }
            else
            {
                throw new Exception("Falha ao editar o veiculo");
            }
        }

        #endregion

    }
}
