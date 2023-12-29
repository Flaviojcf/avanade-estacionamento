using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Interfaces.Repository
{
    public interface IVeiculoRepository : IBaseRepository<VeiculoModel>
    {
        Task<IEnumerable<VeiculoModel>> GetByEstacionamentoId(Guid id);
    }
}
