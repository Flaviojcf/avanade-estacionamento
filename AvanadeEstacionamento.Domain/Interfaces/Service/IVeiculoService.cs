using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Interfaces.Service
{
    public interface IVeiculoService
    {
        Task<VeiculoModel> Create(VeiculoModel estacionamento);
        Task<bool> Update(VeiculoModel estacionamento, Guid id);
        Task<bool> Delete(Guid id);
        Task<VeiculoModel> GetById(Guid id);
        Task<IEnumerable<VeiculoModel>> GetAll();
        Task<IEnumerable<VeiculoModel>> GetByEstacionamentoId(Guid id);
    }
}
