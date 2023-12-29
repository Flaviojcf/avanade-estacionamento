using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Interfaces.Service
{
    public interface IEstacionamentoService
    {
        Task<EstacionamentoModel> Create(EstacionamentoModel estacionamento);
        Task<bool> Update(EstacionamentoModel estacionamento, Guid id);
        Task<bool> Delete(Guid id);
        Task<EstacionamentoModel> GetById(Guid id);
        Task<IEnumerable<EstacionamentoModel>> GetAll();
    }
}
