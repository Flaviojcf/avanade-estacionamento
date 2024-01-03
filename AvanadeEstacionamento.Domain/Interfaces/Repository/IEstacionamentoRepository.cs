using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Interfaces.Repository
{
    public interface IEstacionamentoRepository : IBaseRepository<EstacionamentoModel>
    {
        Task<EstacionamentoModel?> GetByName(string name);
    }
}
