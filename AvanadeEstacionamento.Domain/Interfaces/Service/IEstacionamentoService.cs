using AvanadeEstacionamento.Domain.DTO.Estacionamento;
using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Interfaces.Service
{
    public interface IEstacionamentoService
    {
        Task<EstacionamentoModel> Create(RequestEstacionamentoDTO estacionamentoDTO);
        Task<bool> Update(EstacionamentoModel estacionamento, Guid id);
        Task<bool> Delete(Guid id);
        Task<EstacionamentoModel> GetById(Guid id);
        Task<IEnumerable<EstacionamentoModel>> GetAll();
    }
}
