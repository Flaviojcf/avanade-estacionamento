using AvanadeEstacionamento.Domain.DTO.Veiculo;
using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Interfaces.Service
{
    public interface IVeiculoService
    {
        Task<VeiculoModel> Create(RequestVeiculoDTO veiculoDTO);
        Task<bool> Update(VeiculoModel veiculo, Guid id);
        Task<bool> Delete(Guid id);
        Task<VeiculoModel> GetById(Guid id);
        Task<IEnumerable<VeiculoModel>> GetAll();
        Task<IEnumerable<VeiculoModel>> GetByEstacionamentoId(Guid id);
        Task<bool> GetByPlaca(string placa);
        Task<decimal> GetDebt(Guid id);
        Task<decimal> Checkout(Guid id);
    }
}
