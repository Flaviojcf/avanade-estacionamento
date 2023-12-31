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

        public async Task<decimal> GetDebt(Guid id)
        {
            var veiculoModel = await _veiculoRepository.GetById(id);

            if (veiculoModel == null)
            {
                throw new Exception("Veículo não encontrado");
            }
            else
            {
                var debt = await CalculateDebt(veiculoModel);
                return debt;
            }
        }

        public async Task<VeiculoModel> Create(VeiculoModel veiculo)
        {
            var isAlredyExistsVeiculo = await GetByPlaca(veiculo.Placa);

            if (!isAlredyExistsVeiculo)
            {
                await _veiculoRepository.Create(veiculo);

                return veiculo;
            }
            else
            {
                throw new Exception("Falha ao criar veículo");
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

        public async Task<decimal> Checkout(Guid id)
        {

            var veiculoModel = await _veiculoRepository.GetById(id);

            if (veiculoModel == null)
            {
                throw new Exception("Veículo não encontrado");
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
